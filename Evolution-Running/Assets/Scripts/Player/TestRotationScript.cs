using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotationScript : MonoBehaviour
{
    public GameObject target;
    public float rotationSpeed;

    private float timeCount = 0.0f;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = target.transform.position - transform.position;
        /*Quaternion desireRotation = Quaternion.LookRotation(target.transform.position-transform.position);
        desireRotation = Quaternion.Euler(0,desireRotation.y,0);
        Debug.LogWarning(desireRotation);
        transform.rotation =
           Quaternion.Slerp(transform.rotation,desireRotation,timeCount);
        timeCount = timeCount + Time.deltaTime;
        Debug.DrawLine(transform.position,transform.forward*10,Color.magenta);*/
        
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }
}
