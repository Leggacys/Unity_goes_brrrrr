using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class HitShrink : MonoBehaviour
{
    private int currentHitCount = 0;
    public int lives;
    
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        //rb.AddForce(Vector3.back * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Projectile")
        {
            ProjectileEffect(collision.gameObject);
            
        }

        if (collision.gameObject.tag == "Road")
        {
            transform.parent = collision.gameObject.transform;
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
        
        if (currentHitCount >= lives)
        {
            GameManager.instance.score += 2;
            Destroy(gameObject);
        }
    }
}
