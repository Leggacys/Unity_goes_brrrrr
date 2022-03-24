using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerAnimations : MonoBehaviour
{
    #region Singleton

    public static PlayerAnimations instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public Transform[] rotationPoints;
    public float rotationSpeed;

    private int indexOfPoint=2;
    private bool isRotating;

    private void RotateEffect()
    {
       // Debug.Log("outside the if");
        if (!isRotating)
        {
            //Debug.Log("in RotateEffect");
            isRotating = true;
            StartCoroutine(Rotate());
        }
    }


    private void Update()
    {
        transform.LookAt(rotationPoints[indexOfPoint]);

    }

    IEnumerator Rotate()
    {

        indexOfPoint++;
        if (indexOfPoint >= rotationPoints.Length)
            indexOfPoint = 0;
        isRotating = false;
        
        Debug.LogWarning(rotationPoints[indexOfPoint].gameObject.name);
        
        yield return null;
    }

    private bool CheckRotation()
    {
        Vector3 relativePosition = (rotationPoints[indexOfPoint].position -
                                   transform.position).normalized ;

        float dot = Vector3.Dot(relativePosition, transform.forward);

        if (dot > 0.9)
            return true;
        return false;

    }


    public void FallAnimation()
    {
        LeanTween.scale(gameObject, Vector3.one,  PlayerManager.instace.twinningTime/2)
            .setEaseOutElastic();
    }
    
    public void JumpEffect(GameObject gameObject)
    {
        LeanTween.scale(gameObject, new Vector3(0.9f,0.9f,0.9f), PlayerManager.instace.twinningTime)
            .setEaseOutElastic();
        RotateEffect();
    }
    
}
