using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    public float timeToLive;
    public GameObject explosion;
    
    void Start()
    {
        StartCoroutine(DestroyObj());
    }

    IEnumerator DestroyObj(){
        yield return new WaitForSeconds(timeToLive);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    { 
        Instantiate(explosion, transform.position, Quaternion.identity);
       Destroy(gameObject);
    }

}
