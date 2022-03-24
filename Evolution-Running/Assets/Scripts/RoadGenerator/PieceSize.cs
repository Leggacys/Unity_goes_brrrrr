﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSize : MonoBehaviour
{
    public float maxScanLength;
    public float ratio;
    public float width,maxHeight,minHeight,endpointRelative;
    //public LayerMask layers;
    public bool isFirstTime;
    public PieceGenerator generator;
    void Start(){
        
        if(isFirstTime)
        StartCoroutine(getCoords());

    }

    IEnumerator getCoords(){


        Vector3 RayPosition = transform.position + Vector3.up * 10;
        Vector3 hitPoint;
        Vector3 DownDirection;
        Vector3 lastHit = new Vector3(0,0,0);
        int count = 0;
        for(float i = 0; i < maxScanLength; i+=ratio) {
             RaycastHit hit;
            //layers = ~layers;
             RayPosition += new Vector3(0.05f,0,0);
            DownDirection = Vector3.down;
            Ray ray = new Ray(RayPosition,DownDirection);
            if(Physics.Raycast(ray,out hit,40)){
                //Debug.Log("Hit");
                count ++;
                //if(hit.transform.parent == this){
                    //Debug.Log("Here");
                    hitPoint = hit.point;
                    float targetY = hitPoint.y -transform.position.y;
                    if(targetY < minHeight)
                        minHeight = targetY;
                    if(targetY > maxHeight)
                        maxHeight = targetY;
                    lastHit = hit.point;
                //}
            }
            //Debug.Log("Start " + RayPosition);
            //Debug.Log("End " + DownDirection);
            Debug.DrawRay(RayPosition,DownDirection+Vector3.down * 10,Color.green,1f,true);
           //Debug.Log("Trying"); 
           //Debug.DrawLine(RayPosition, DownDirection,Color.white,0.5f);
           yield return new WaitForSeconds(0.001f);
        }
        width = Mathf.Abs(transform.position.x - lastHit.x);
        endpointRelative = lastHit.y - transform.position.y;
        //Debug.Log(count);

        for(int i = 0; i < 5; i++) {
            if(generator.inactivePool == null)
                generator.inactivePool = new List<GameObject>();
            GameObject generated = Instantiate(this.gameObject,this.transform.position,this.transform.rotation);
            generated.GetComponent<PieceSize>().isFirstTime = false;
            
            generator.inactivePool.Add(generated);
            
        }

    }
    

}
