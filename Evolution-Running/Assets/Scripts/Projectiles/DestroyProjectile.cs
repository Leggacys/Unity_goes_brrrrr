using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{

    public float timeToLive;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObj());
    }

    IEnumerator DestroyObj(){

        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }


}
