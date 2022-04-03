using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBossProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        
        if (collision.gameObject.tag == "Road")
        {
            transform.parent = collision.gameObject.transform;
        }
    }
}
