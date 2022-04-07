using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform player;
    public Transform bossMouth;
    public float minInterval, maxInterval;
    public float force;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(shootAt());
    }

    IEnumerator shootAt()
    {
        while (true)
        {
            
            GameObject proj = Instantiate(projectile, bossMouth.position, Quaternion.identity);
            animator.SetBool("isAttacking",true);
            Rigidbody rb = proj.GetComponent<Rigidbody>();

            float offset = Random.Range(-1f, 2f);
            Vector3 pos = player.position - bossMouth.position + new Vector3(0,offset,0);
            //rb.AddForce(pos + new Vector3(0, -offset, 0));
            rb.velocity = pos.normalized * force;
            
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("isAttacking",false);
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
