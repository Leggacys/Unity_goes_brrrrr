using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    public Vector3 startPoint;

    public List<GameObject> currentPieces=null;
    public List<GameObject> inactivePool=null;
    public float pieceSpeed;
    public float maxHeight,minHeight,length,initialLength,workingLength;
    Vector3 latestOffset;
    public int maxTries;
    // Start is called before the first frame update
    void Awake()
    {
        //currentPieces = new List<GameObject>();
        //inactivePool = new List<GameObject>();
        
        // foreach(PieceSize obj in sizes) {
        //     Debug.Log(obj.width);
            
        // }
        
    }

    void Start(){
        startPoint = new Vector3(0,0,0);
        StartCoroutine(checkPoolSize());
    }

    IEnumerator checkPoolSize(){
        while (true){
            if(inactivePool.Count>initialLength){
                StartCoroutine(roadGenerator());
                break;
            }
        yield return new WaitForSeconds(0.05f);
        }
    }

    // void Initial(){
    //     for(int i = 0; i < initialLength; i++) {
    //         PickPiece();
    //     }
    //     StartCoroutine(roadGenerator());
    // }

    void Update(){
        if(currentPieces.Count > 0){
            
            startPoint=currentPieces[currentPieces.Count-1].transform.position + latestOffset;
        }
    }



    public void PickPiece(){
        
        int tries = 0;
        bool succesfull = false;
        while(!succesfull){

            int pickIndex = Random.Range(0,inactivePool.Count);
            PieceSize size = inactivePool[pickIndex].GetComponent<PieceSize>();
            if(startPoint.y + size.minHeight > minHeight  && startPoint.y + size.maxHeight < maxHeight){
                //startPoint += new Vector3(sizes[pickIndex].width,0,0f);

                
                inactivePool[pickIndex].transform.position = startPoint;
                
                inactivePool[pickIndex].SetActive(true);
                currentPieces.Add(inactivePool[pickIndex]);
                inactivePool[pickIndex].GetComponent<MovePiece>().speed= pieceSpeed;
                inactivePool[pickIndex].GetComponent<MovePiece>().isRunning=true;
                inactivePool.RemoveAt(pickIndex);
                // GameObject generated = Instantiate(pieces[pickIndex],startPoint,Quaternion.identity);
                // generated.GetComponent<PieceSize>().isFirstTime = false;
                // generated.SetActive(true);
                // generated.GetComponent<PieceView>().enabled = true;
                succesfull = true;
                startPoint += new Vector3(size.width,size.endpointRelative,0);
                latestOffset = new Vector3(size.width,size.endpointRelative,0);
                
            }

            tries++;
            if(tries >maxTries)
                succesfull = true;
        }


    }

    IEnumerator roadGenerator(){

        while(true){

            if(currentPieces.Count < workingLength)
                PickPiece();


            yield return new WaitForSeconds(0.05f);
        }


    }

    public void removeFromPool(GameObject obj){
        if(currentPieces.Contains(obj)){
        currentPieces.Remove(obj);
        inactivePool.Add(obj);
        obj.GetComponent<MovePiece>().isRunning=false;
        obj.SetActive(false);
        }
    }

   
}
