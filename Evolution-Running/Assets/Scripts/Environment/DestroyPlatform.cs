using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour,IDestroyBase
{
    public GameObject crackedPrefab = null;
    public GameObject fullPrefab;
    public GameObject powers;
    public void onDestruction(float slamForce, float radius, Vector3 position)
    {
        PlatformRegeneration regen = GetComponent<PlatformRegeneration>();
        if (regen)
        {
            regen.onDestruction();
        }

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

            count = powers.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Rigidbody rb = powers.transform.GetChild(i).GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.constraints = RigidbodyConstraints.None;
                    rb.isKinematic = false;
                    //hit.gameObject.transform.parent = null;
                    rb.AddExplosionForce(slamForce * radius, position, radius);
                }
            }
        }
    }
}
