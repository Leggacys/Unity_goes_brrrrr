using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class HitShrink : MonoBehaviour
{
    private int currentHitCount = 0;
    public int lives;
    public float speed;
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        rb.AddForce(Vector3.back * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Projectile")
        {
            ProjectileEffect(collision.gameObject);
            
        }


        
        
    }

    private void ProjectileEffect(GameObject projectile)
    {
        LeanTween.scale(gameObject, transform.localScale + new Vector3(0.1f, 0.1f, 0.1f), 0.05f)
            .setEaseInCubic()
            .setOnComplete(() =>
            {
                LeanTween.scale(gameObject, transform.localScale - new Vector3(0.2f, 0.2f, 0.2f), 0.3f)
                    .setEaseInElastic();
            });

        currentHitCount++;
        
        Destroy(projectile);
        if(currentHitCount>= lives)
            Destroy(gameObject);
    }
}
