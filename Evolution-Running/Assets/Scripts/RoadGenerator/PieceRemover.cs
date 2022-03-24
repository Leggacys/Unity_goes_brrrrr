using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceRemover : MonoBehaviour
{

    public PieceGenerator generator;
    
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Collided");
        generator.removeFromPool(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        //Debug.Log("Collided");
        generator.removeFromPool(other.gameObject);
    }
}
