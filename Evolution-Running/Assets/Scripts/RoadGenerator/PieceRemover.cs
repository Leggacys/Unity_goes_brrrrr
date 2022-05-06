using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceRemover : MonoBehaviour
{

    //public PieceGenerator generator;
    public float piecesPassed = 0;
    private PieceGenerator generator;
    private GameManager gameManager;
    private void Start(){
    
        generator = PieceGenerator.instance;
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Collided");
        if (other.gameObject.tag == "Road" && other.gameObject.transform.parent == null)
        {
            generator.removeFromPool(other.gameObject);
            
        }

        //if(other.gameObject.tag == "Enemy")
        //    Destroy(other.gameObject);
    }

    //private void OnTriggerExit(Collider other) {
        //Debug.Log("Collided");
    //    generator.removeFromPool(other.gameObject);
    //}
}
