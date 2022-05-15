using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySimple : MonoBehaviour,IDestroyBase
{
    public GameObject crackedPrefab = null;
    public GameObject fullPrefab;
    public GameObject copy;
    private Transform parent;
    private Vector3 offset;
    private Quaternion originalRotation;

    private void Start()
    {
        parent = transform.parent;
        offset = transform.localPosition;
        originalRotation = transform.rotation;
    }

    public void onDestruction(float slamForce, float radius, Vector3 position)
    {
        
        int countt = fullPrefab.transform.childCount;
        for (int i = 0; i < countt; i++)
        {
            if (fullPrefab.transform.GetChild(i).tag == "Enemy")
            {
                fullPrefab.transform.GetChild(i).parent = null;
            }
        }
        fullPrefab.SetActive(false);
        if (crackedPrefab)
        {
            crackedPrefab.SetActive(true);
            
            int count = crackedPrefab.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Rigidbody rb = crackedPrefab.transform.GetChild(i).GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.constraints = RigidbodyConstraints.None;
                    rb.isKinematic = false;
                    //hit.gameObject.transform.parent = null;
                    rb.AddExplosionForce(slamForce * radius, position, radius);
                }
            }

            StartCoroutine(cleanUp());
        }
    }

    IEnumerator cleanUp()
    {
        yield return new WaitForSeconds(5f);
        GameObject spawned =Instantiate(copy, parent.position + offset, originalRotation);
        spawned.transform.parent = parent;
        spawned.transform.localPosition = offset;
        Destroy(gameObject);
    }
}
