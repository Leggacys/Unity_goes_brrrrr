using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerActivation : MonoBehaviour
{
    public int powerCase;
    private PowerManager powerManager;
    private void Start()
    {
        powerManager=PowerManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (powerCase)
            {
                case 0:
                    powerManager.HealthPower();
                    break;
                case 1:
                    powerManager.SpeedPower();
                    break;
                case 2:
                    powerManager.JumpPower();
                    break;
                case 3:
                    powerManager.ProjectilePower();
                    break;
                
            }

            Destroy(gameObject);
        }
    }
}
