using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform player;
    public float minInterval, maxInterval;
    public float force;
    void Awake()
    {
        StartCoroutine(shootAt());
    }

    IEnumerator shootAt()
    {
        while (true)
        {
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody rb = proj.GetComponent<Rigidbody>();

            float offset = Random.Range(-1f, 2f);
            Vector3 pos = player.position - transform.position + new Vector3(0,offset,0);
            //rb.AddForce(pos + new Vector3(0, -offset, 0));
            rb.velocity = pos.normalized * force;
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
