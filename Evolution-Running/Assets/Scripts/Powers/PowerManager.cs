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

    private float initialSpeed;
    private float initialJump;
    private PlayerManager playerManager;
    
    
    void Start()
    {
        playerManager = PlayerManager.instace;
        initialSpeed = playerManager.speed;
        initialJump = playerManager.jumpAmount;
    }
    
    public void HealthPower()
    {
        
        playerManager.maxHP += healingPower;
        playerManager.hpBar.SetHealth(playerManager.maxHP);
    }

    public void SpeedPower()
    {
        StopCoroutine(SpeedUp());
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
        playerManager.jumpAmount = initialJump;
        StartCoroutine(JumpUp());

    }

    IEnumerator JumpUp()
    {
        
        
        playerManager.jumpAmount += jumpPower;
        yield return new WaitForSeconds(jumpTime);
        playerManager.jumpAmount = initialJump;
    }
}
