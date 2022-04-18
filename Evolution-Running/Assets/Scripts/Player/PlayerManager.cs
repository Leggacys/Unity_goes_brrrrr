using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    #region Singletone

    public static PlayerManager instace;
    
    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }else if (instace != this)
        {
            Destroy(gameObject);
        }

        playerInput = new PlayerInput();
    }
    #endregion


    public PlayerInput playerInput;
    public float twinningTime;
    public float jumpAmount = 1f;
    public float speed;
    public float rotatePerJump;
    public GameObject playerBody;

    [Header("Health")] 
    public int maxHP;
    public int damagePerHit;
    public HealthBar hpBar;
    public int currentHP;
    
    
    public void JumpAnimation()
    {
        if (PlayerMovement.instance.isGrounded)
        {
            PlayerMovement.instance.Jump();
        }
    }

    public void AttackMove()
    {
        if(PlayerMovement.instance.canAttack){
            PlayerMovement.instance.canAttack = false;
            PlayerMovement.instance.Attack();
        }

    }

    public void DashMove()
    {
        if (PlayerMovement.instance.canDash)
        {
            PlayerMovement.instance.canDash = false;
            PlayerMovement.instance.Dash();
        }
    }

    public void GroundMove()
    {
        PlayerMovement.instance.Land();
    }

    private void Start()
    {
        playerInput.Movement.Jump.performed += context => JumpAnimation();
        playerInput.Movement.Throw.performed += context => AttackMove();
        playerInput.Movement.Dash.performed += context => DashMove();
        playerInput.Movement.GroundPound.performed += context => GroundMove();
        hpBar.SetMaxHealth(maxHP);
        currentHP = maxHP;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHP -= damagePerHit;
            hpBar.SetHealth(currentHP);
            StartCoroutine(HitEffect());
        }

        if (currentHP <= 0)
        {
            GameManager.instance.OnDeath();
        }

    }

    IEnumerator HitEffect()
    { 
        Debug.Log("set take hit " + PlayerMovement.instance.takeHit);
        PlayerMovement.instance.TakeHitEffect();
        PlayerMovement.instance.takeHit = true;

        yield return new WaitForSeconds(1f);

        PlayerMovement.instance.takeHit = false;
    }
}
