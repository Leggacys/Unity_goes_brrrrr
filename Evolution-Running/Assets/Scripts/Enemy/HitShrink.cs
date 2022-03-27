using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitShrink : MonoBehaviour
{
    private int currentHitCount = 0;
    public int lives;
    private bool firstTouch = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (firstTouch)
        {
            GetComponent<Rigidbody>().velocity =new Vector3(0,0,0);
            firstTouch = false;
        }
        if (collision.gameObject.tag == "Projectile")
        {
            currentHitCount++;
            Destroy(collision.gameObject);
            LeanTween.scale(gameObject, transform.localScale + new Vector3(0.1f, 0.1f, 0.1f), 0.05f)
                .setEaseInCubic()
                .setOnComplete(() =>
                {
                    LeanTween.scale(gameObject, transform.localScale - new Vector3(0.2f, 0.2f, 0.2f), 0.3f)
                        .setEaseInElastic();
                });
            if(currentHitCount>= lives)
                Destroy(gameObject);
            
        }
    }
}
