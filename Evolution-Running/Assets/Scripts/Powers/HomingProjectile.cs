using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed;
    public float torque;
    private Rigidbody rb;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(GetTarget());
    }

    IEnumerator GetTarget()
    {
        while (true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 50);
            hitColliders = hitColliders.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position))
                .ToArray();

            foreach (Collider hit in hitColliders)
            {
                if (hit.tag.Contains("Enemy"))
                {



                    target = hit.gameObject.transform;
                    break;
                }

            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.LookAt(target);
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
            //rb.MovePosition((target.position-transform.position)*speed * Time.deltaTime);
            //rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            //var lookPos = target.position - transform.position;
            //lookPos.y = 0;
            //var rotation = Quaternion.LookRotation(lookPos);
            //rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, torque * Time.deltaTime));
        }
    }
}
