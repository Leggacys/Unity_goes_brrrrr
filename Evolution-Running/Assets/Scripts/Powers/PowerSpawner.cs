using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    public float spawnChance;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.value < spawnChance)
        {
            int index = Random.Range(0, 3);
            transform.GetChild(index).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
