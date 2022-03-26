using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotationScript : MonoBehaviour
{
    public float rotationSpeed;
    public Vector3 degreeAdded;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        rotation += degreeAdded;
        
        Quaternion newRotation = Quaternion.Euler(rotation);
        
        transform.rotation = Quaternion.Lerp(transform.rotation,newRotation,rotationSpeed*Time.deltaTime);

    }
}
