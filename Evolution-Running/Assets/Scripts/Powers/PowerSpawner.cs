using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    public float spawnChance;
    // Start is called before the first frame update
    public void OnEnable()
    {
        if (Random.value < spawnChance)
        {
            checkActive();
            
                int index = Random.Range(0, transform.childCount);
                transform.GetChild(index).gameObject.SetActive(true);
            
        }
    }

    void checkActive()
    {
        for(int i = 0;i<transform.childCount;i++)
            if (transform.GetChild(i).gameObject.activeSelf)
                transform.GetChild(i).gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
