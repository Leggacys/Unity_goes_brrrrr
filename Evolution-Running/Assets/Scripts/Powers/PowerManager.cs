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

    private float initialSpeed;
    private float initialJump;
    private PlayerMovement playerMovement;
    
    
    public void StartDataCollection()
    {
        playerMovement = PlayerMovement.instance;
        initialSpeed = PlayerManager.instace.speed;
        initialJump = PlayerManager.instace.jumpAmount;
    }
    
    public void HealthPower()
    {
        if (PlayerManager.instace.currentHP < PlayerManager.instace.maxHP)
        {
            PlayerManager.instace.currentHP += healingPower;
            PlayerManager.instace.hpBar.SetHealth(PlayerManager.instace.currentHP);
        }
    }

    public void SpeedPower()
    {
        StopCoroutine(SpeedUp());
        PlayerManager.instace.speed = initialSpeed;
        StartCoroutine(SpeedUp());

    }

    IEnumerator SpeedUp()
    {
        PlayerManager.instace.speed += speedPower;
        yield return new WaitForSeconds(speedTime);
        PlayerManager.instace.speed = initialSpeed;
    }

    public void JumpPower()
    {
        StopCoroutine(JumpUp());
        PlayerManager.instace.jumpAmount = initialJump;
        StartCoroutine(JumpUp());

    }

    IEnumerator JumpUp()
    {
        
        
        PlayerManager.instace.jumpAmount += jumpPower;
        yield return new WaitForSeconds(jumpTime);
        PlayerManager.instace.jumpAmount = initialJump;
    }

    public void ProjectilePower()
    {
        playerMovement.homingProjectiles += specialProjectiles;
    }
}
