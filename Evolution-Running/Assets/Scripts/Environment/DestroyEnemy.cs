using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour,IDestroyBase
{
    public void onDestruction(float slamForce, float radius, Vector3 position)
    {
        StartCoroutine(destroyOverTime(slamForce,radius,position));
    }

    IEnumerator destroyOverTime(float slamForce, float radius, Vector3 position)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.isKinematic = false;
            //hit.gameObject.transform.parent = null;
            rb.AddExplosionForce(slamForce * radius, position, radius);
        }

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
