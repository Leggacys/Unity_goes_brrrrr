using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    public float spawnChance;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (Random.value < spawnChance)
        {
            if (!checkActive())
            {
                int index = Random.Range(0, transform.childCount);
                transform.GetChild(index).gameObject.SetActive(true);
            }
        }
    }

    bool checkActive()
    {
        for(int i = 0;i<transform.childCount;i++)
            if (transform.GetChild(i).gameObject.activeSelf)
                return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
