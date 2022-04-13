using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;

    public float spawnInterval;
    public Vector3 offset;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnInterval());
    }

    IEnumerator SpawnInterval()
    {
        while (true)
        {
            Vector3 pos = spawnPoint.position +offset;
            GameObject spawned = Instantiate(enemy, pos, Quaternion.Euler(0,-90,0));
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
