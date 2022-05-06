using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    
    #region Singleton

    public static PowerManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }
	

    #endregion
    
    public int healingPower;
    
    public float speedPower;
    public float speedTime;

    public float jumpPower;
    public float jumpTime;

    public float specialProjectiles;

    public GameObject player;
    private float initialSpeed;
    private float initialJump;
    private PlayerManager playerManager;
    private PlayerMovement playerMovement;
    
    
    public void StartDataCollection()
    {
        playerManager = PlayerManager.instace;
        playerMovement = PlayerMovement.instance;
        initialSpeed = playerManager.speed;
        initialJump = playerManager.jumpAmount;
    }
    
    public void HealthPower()
    {
        if (playerManager.currentHP < playerManager.maxHP)
        {
            playerManager.currentHP += healingPower;
            playerManager.hpBar.SetHealth(playerManager.currentHP);
        }

        LeanTween.scale(player, new Vector3(1.5f, 1.5f, 1.5f), 0.3f).setEaseOutBounce();
    }

    public void SpeedPower()
    {
        StopCoroutine(SpeedUp());
        LeanTween.scale(player, new Vector3(0.5f, 0.5f, 2f), 0.4f).setEaseOutBounce();
        playerManager.speed = initialSpeed;
        StartCoroutine(SpeedUp());

    }

    IEnumerator SpeedUp()
    {
        
        
        playerManager.speed += speedPower;
        yield return new WaitForSeconds(speedTime);
        playerManager.speed = initialSpeed;
    }

    public void JumpPower()
    {
        StopCoroutine(JumpUp());
        LeanTween.scale(player, new Vector3(0.5f, 2, 0.5f), 0.4f).setEaseOutBounce();
        playerManager.jumpAmount = initialJump;
        StartCoroutine(JumpUp());

    }

    IEnumerator JumpUp()
    {
        
        
        playerManager.jumpAmount += jumpPower;
        yield return new WaitForSeconds(jumpTime);
        playerManager.jumpAmount = initialJump;
    }

    public void ProjectilePower()
    {
        playerMovement.homingProjectiles += specialProjectiles;
    }
}
