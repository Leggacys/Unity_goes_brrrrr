using System;
using System.Collections;
using System.Security.Cryptography;
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

    private void Start()
    {
        playerInput.Movement.Jump.performed += context => JumpAnimation();
        playerInput.Movement.Throw.performed += context => AttackMove();
        playerInput.Movement.Dash.performed += context => DashMove();
        hpBar.SetMaxHealth(maxHP);
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
            maxHP -= damagePerHit;
            hpBar.SetHealth(maxHP);
        }
        
    }
}
