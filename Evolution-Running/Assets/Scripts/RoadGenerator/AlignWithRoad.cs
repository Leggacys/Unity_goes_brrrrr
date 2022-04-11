using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithRoad : MonoBehaviour
{
    public float width;

    public float angle;

    public LayerMask layerMask;
    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("Neata");
        //ComputeAngle();
        StartCoroutine(Align());

    }

    IEnumerator Align()
    {
        yield return new WaitForSeconds(1f);
        ComputeAngle();
    }

    

    void ComputeAngle()
    {
        Vector3 startingPoint = transform.position - new Vector3(0,0,width/2);
        Vector3 endPoint = startingPoint + new Vector3(0, 0, width);
        
        RaycastHit hit;

        Vector3 hitPoint;
        Vector3 DownDirection = Vector3.down;
        float h1, h2;
        h1 = 0;
        h2 = 0;
        Ray ray = new Ray(startingPoint,DownDirection);
        if (Physics.Raycast(ray, out hit, 100,layerMask))
        {
            hitPoint = hit.point;
            h1 = startingPoint.y - hitPoint.y;
        }
        

        
        ray = new Ray(endPoint,DownDirection);
        if (Physics.Raycast(ray, out hit, 100,layerMask))
        {
            hitPoint = hit.point;
            h2 = endPoint.y - hitPoint.y;
        }
        


        float h;
        h = Mathf.Abs(h1 - h2);

        float hyp;
        hyp = Mathf.Sqrt(width * width + h * h);
        angle = (Mathf.Asin(width / hyp)) * Mathf.Rad2Deg;
        //if (angle < 90 && angle>0)
        //    angle = 90-angle;

        if (h1 - h2 >= 0)
            angle = angle-90;
        else
        {
            angle = 90 - angle;
        }

   

        transform.rotation = Quaternion.Euler(new Vector3(angle,0,0));
    }
}
