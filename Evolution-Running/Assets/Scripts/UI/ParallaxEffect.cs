using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startPos;
    public GameObject camera;
    public float parallaxEffect;
    
    
    // Start is called before the first frame update
    void Start() 
    {
    startPos = transform.position.z;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (camera.transform.position.z - (1 * parallaxEffect));
        float dist = camera.transform.position.z * parallaxEffect;
        transform.position = new Vector3(transform.position.x, transform.position.y, dist+startPos);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
