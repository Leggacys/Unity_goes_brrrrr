using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{
    public bool isRunning;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        if(isRunning)
            this.transform.position -= new Vector3(speed * Time.deltaTime,0,0);  
    }
}
