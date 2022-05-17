using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHover : MonoBehaviour
{
        public float bossHeight;
        public LayerMask ignoreLayer;
        private float lastHeight, newHeight;
        
        
        void OnEnable()
        {
            
            StartCoroutine(initialHeight());
    
    
    
    
        }
    
        IEnumerator initialHeight()
        {
            yield return new WaitForSeconds(0.3f);
            lastHeight = transform.position.y;
            Vector3 oldPos = transform.localPosition;
            transform.position += new Vector3(0, 100, 0);
            Vector3 RayPosition = transform.position - Vector3.up * 30;
            
            Vector3 hitPoint;
            Vector3 DownDirection;
    
            Debug.Log(transform.position);
            RaycastHit hit;
    
    
            DownDirection = Vector3.down;
            Ray ray = new Ray(RayPosition, DownDirection);
            Debug.DrawRay(RayPosition,DownDirection+Vector3.down * 100,Color.green,10f);
            Debug.Log(RayPosition);
            Debug.Log(DownDirection);
            //bloomLayer = ~bloomLayer;
            if (Physics.Raycast(ray, out hit, 200,ignoreLayer))
            {
                hitPoint = hit.point;
                transform.position = new Vector3(transform.position.x, hit.point.y + bossHeight,
                    transform.position.z);
                Debug.Log("HIt " + hit.collider.gameObject.name );
            }
    
            StartCoroutine(updateHeight());
        }
    
    
    
        IEnumerator updateHeight()
        {
            float newScanHeight;
            while (true)
            {
                newScanHeight = scanHeight();
                if (newScanHeight != -999 && newScanHeight != lastHeight)
                {
                    StopCoroutine(raiseToHeight());
                    lastHeight = newScanHeight;
                    StartCoroutine(raiseToHeight());
                }
    
                yield return new WaitForSeconds(0.3f);
            }
        }
    
        private float scanHeight()
        {
            Vector3 RayPosition = transform.position + Vector3.up * 25 +new Vector3(0,0,7f);
            
            Vector3 hitPoint;
            Vector3 DownDirection;
    
            Debug.Log(transform.position);
            RaycastHit hit;
    
    
            DownDirection = Vector3.down;
            Ray ray = new Ray(RayPosition, DownDirection);
            Debug.DrawRay(RayPosition,DownDirection+Vector3.down * 100,Color.green,10f);
            Debug.Log(RayPosition);
            Debug.Log(DownDirection);
            //bloomLayer = ~bloomLayer;
            if (Physics.Raycast(ray, out hit, 200,ignoreLayer))
            {
                
                hitPoint = hit.point;
                //transform.position = new Vector3(transform.position.x, hit.point.y + bossHeight,
                //    transform.position.z);
                Debug.Log("HIt " + hit.collider.gameObject.name );
                return hit.point.y;
            }
    
            return -999;
        }
    
        IEnumerator raiseToHeight()
        {
            while (Math.Abs((lastHeight+bossHeight) - transform.position.y) > 4f)
            {
                float increment = 0.1f;
                if (lastHeight < transform.position.y)
                    increment = -increment;
                transform.position += new Vector3(0, increment, 0);
                    
                yield return new WaitForSeconds(0.002f);
            }
            yield return null;
        }
}
