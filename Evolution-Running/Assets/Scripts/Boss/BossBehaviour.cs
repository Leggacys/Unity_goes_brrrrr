using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform player;
    public Transform bossMouth;
    public float minInterval, maxInterval;
    public float force;
    
    [Header("Health")] 
    public int maxHP;
    public int damagePerHit;
    public HealthBar hpBar;
    
    
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        hpBar.SetMaxHealth(maxHP);
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

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Collided");
        if (collision.gameObject.tag == "Projectile")
        {
            maxHP -= damagePerHit;
            hpBar.SetHealth(maxHP);
        }

        if (maxHP < 0)
        {
            
            GameManager.instance.OnBossDefeat();
            gameObject.SetActive(false);
        }
    }
}
