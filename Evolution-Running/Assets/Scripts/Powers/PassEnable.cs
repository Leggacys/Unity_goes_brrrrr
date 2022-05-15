using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassEnable : MonoBehaviour
{
    public GameObject powerBundle;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //Debug.Log("MASM ASCTIVATAS");
        powerBundle.GetComponent<PowerSpawner>().OnEnable();
    }
}
