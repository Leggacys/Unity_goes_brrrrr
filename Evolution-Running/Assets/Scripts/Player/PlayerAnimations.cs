using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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

    public List<float> angles;
    public float tweeningTime;
    
    private bool isRotating;
    private int index = 0;
    private bool reverse;
    
    private void RotateEffect()
    {
       // Debug.Log("outside the if");
        if (!isRotating)
        {
            //Debug.Log("in RotateEffect");
            isRotating = true;
            LeanTween.rotateX(gameObject, angles[index], tweeningTime)
                .setEaseInCubic()
                .setOnComplete(FinishingRotation);
        }
    }

    private void FinishingRotation()
    {
        if (reverse)
        {
            index--;
        }
        else
        {
            index++;
        }
        
        isRotating = false;
        if (index + index >= angles.Count || index -1 < 0)
            reverse = !reverse;
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
