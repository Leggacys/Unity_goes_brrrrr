using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerActivation : MonoBehaviour
{
    public int powerCase;
    private PowerManager powerManager;
    //private GameObject player;
    public GameObject particles;
    private void Start()
    {
        powerManager=PowerManager.instance;
        //particleSystem = GetComponent<ParticleSystem>();
        //player = GameObject.FindWithTag("Player");
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

            if (particles)
            {
                Debug.Log("Found power");
                GameObject part =Instantiate(particles, transform.position, Quaternion.identity);
                if(powerCase==3)
                    FadeObject(part);
                part.transform.position = other.transform.position;
                part.transform.parent = other.transform;
            }
            Destroy(gameObject);
            //StartCoroutine(destroyDelay());
        }
    }

    void FadeObject(GameObject obj)
    {
        GameObject target = obj.transform.GetChild(0).gameObject;

        LeanTween.alpha(target, 1, 1.5f).setOnComplete(() =>
        {
            LeanTween.alpha(target, 0, 1.5f);
        });

    }
}
