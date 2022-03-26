using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitShrink : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            LeanTween.scale(gameObject, transform.localScale + new Vector3(0.1f, 0.1f, 0.1f), 0.2f)
                .setEaseInCubic()
                .setOnComplete(() =>
                {
                    LeanTween.scale(gameObject, transform.localScale - new Vector3(0.2f, 0.2f, 0.2f), 0.5f)
                        .setEaseInElastic();
                });
            
        }
    }
}
