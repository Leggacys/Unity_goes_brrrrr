using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    
    
    #region Singletone

    public static PieceGenerator instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }

        //playerInput = new PlayerInput();
    }
    #endregion
    
    public Vector3 startPoint;

    public List<GameObject> currentPieces=null;
    public List<GameObject> inactivePool=null;
    public List<GameObject> activeBossFightPieces = null;
    public List<GameObject> inactiveBossPieces = null;
    private List<GameObject> platforms;
    public float pieceSpeed;
    public float maxHeight,minHeight,length,initialLength,workingLength;
    Vector3 latestOffset;
    public int maxTries;

    public GameObject newPlatform;
    public float platformChance;
    public float heightOffset;
    public float previousHeight;

    private int workingCase = 0;
    private GameManager gameManager;

    private Coroutine bossRoutine;

    private Coroutine normalRoutine;
    // Start is called before the first frame update


    void Start(){
        startPoint = new Vector3(0,0,0);
        platforms = new List<GameObject>();
        gameManager = GameManager.instance;
        //bossRoutine = StartCoroutine(roadGenerator(inactiveBossPieces, activeBossFightPieces));
        //StopCoroutine(bossRoutine);
        //normalRoutine = StartCoroutine(roadGenerator(inactivePool, currentPieces));
        //StopCoroutine(normalRoutine);
        StartCoroutine(checkPoolSize());
    }

    IEnumerator checkPoolSize(){
        while (true){
            if(inactivePool.Count>initialLength){
                SwitchMode(1);
                break;
            }
        yield return new WaitForSeconds(0.05f);
        }
    }

    public void SwitchMode(int id)
    {
        switch(id)
        {
            case 0:
                workingCase = 0;
                if(normalRoutine != null)
                    StopCoroutine(normalRoutine);
                //StopCoroutine(roadGenerator(inactivePool,currentPieces));
                for(int i = 0 ; i < currentPieces.Count;i++)
                {
                    GameObject piece = currentPieces[i];
                        currentPieces.Remove(piece);
                        i--;
                        inactivePool.Add(piece);
                        piece.GetComponent<MovePiece>().isRunning=false;
                        piece.SetActive(false);
                    
                }

                foreach (GameObject el in platforms)
                {
                    if(el != null)
                        Destroy(el);
                }

                platforms = new List<GameObject>();
                previousHeight = -999;
                bossRoutine = StartCoroutine(roadGenerator(inactiveBossPieces, activeBossFightPieces));
                //StartCoroutine(roadGenerator(inactiveBossPieces, activeBossFightPieces));
                
                break;
            case 1:
                workingCase = 1;
                if(bossRoutine != null)
                    StopCoroutine(bossRoutine);
                //StopCoroutine(roadGenerator(inactiveBossPieces, activeBossFightPieces));
                for(int i = 0 ;i<activeBossFightPieces.Count;i++)
                {

                    GameObject piece = activeBossFightPieces[i];
                        activeBossFightPieces.Remove(piece);
                        i--;
                        inactiveBossPieces.Add(piece);
                        piece.GetComponent<MovePiece>().isRunning=false;
                        piece.SetActive(false);
                    
                }
                foreach (GameObject el in platforms)
                {
                    if(el != null)
                        Destroy(el);
                }

                platforms = new List<GameObject>();

                previousHeight = -999;
                normalRoutine = StartCoroutine(roadGenerator(inactivePool, currentPieces));
                //StartCoroutine(roadGenerator(inactivePool,currentPieces));
                break;
                
        }
    }

    // void Initial(){
    //     for(int i = 0; i < initialLength; i++) {
    //         PickPiece();
    //     }
    //     StartCoroutine(roadGenerator());
    // }

    void Update(){
        if(workingCase == 1)
            if(currentPieces.Count > 0){
            
            startPoint=currentPieces[currentPieces.Count-1].transform.position + latestOffset;
            }

        if (workingCase == 0)
        {
            if(activeBossFightPieces.Count > 0){
            
                startPoint=activeBossFightPieces[activeBossFightPieces.Count-1].transform.position + latestOffset;
            }
        }
    }



    public void PickPiece(List<GameObject> inactivePool,List<GameObject> activePool){
        
        int tries = 0;
        bool succesfull = false;
        while(!succesfull){

            int pickIndex = Random.Range(0,inactivePool.Count);
            PieceSize size = inactivePool[pickIndex].GetComponent<PieceSize>();
            if(startPoint.y + size.minHeight > minHeight  && startPoint.y + size.maxHeight < maxHeight){
                //startPoint += new Vector3(sizes[pickIndex].width,0,0f);

                
                inactivePool[pickIndex].transform.position = startPoint;
                
                inactivePool[pickIndex].SetActive(true);
                activePool.Add(inactivePool[pickIndex]);
                inactivePool[pickIndex].GetComponent<MovePiece>().speed= pieceSpeed;
                inactivePool[pickIndex].GetComponent<MovePiece>().isRunning=true;
                inactivePool.RemoveAt(pickIndex);
                // GameObject generated = Instantiate(pieces[pickIndex],startPoint,Quaternion.identity);
                // generated.GetComponent<PieceSize>().isFirstTime = false;
                // generated.SetActive(true);
                // generated.GetComponent<PieceView>().enabled = true;
                succesfull = true;
                for (int i = 0; i < 2; i++)
                {
                    if (Random.value < platformChance)
                    {
                        List<float> heights = new List<float>();
                        if (startPoint.y + size.maxHeight + heightOffset < maxHeight)
                            heights.Add(startPoint.y + size.maxHeight + heightOffset);
                        if (previousHeight != -999)
                        {
                            if (previousHeight + heightOffset < maxHeight)
                                heights.Add(previousHeight + heightOffset);
                            if (previousHeight - heightOffset > startPoint.y + size.maxHeight + heightOffset)
                                heights.Add(previousHeight - heightOffset);
                        }

                        if (heights.Count > 0)
                        {
                            int ind = Random.Range(0, heights.Count);
                            float widthOffset;
                            if (i == 0)
                                widthOffset = size.width / 4;
                            else
                                widthOffset = 3 * size.width / 4;
                            
                            Vector3 pos = new Vector3(startPoint.x, heights[ind], startPoint.z +widthOffset);
                            GameObject spawn = Instantiate(newPlatform, pos, Quaternion.identity);
                            platforms.Add(spawn);
                            spawn.GetComponent<MovePiece>().speed = pieceSpeed;
                            spawn.GetComponent<MovePiece>().isRunning = true;
                            previousHeight = heights[ind];
                        }
                        else
                        {
                            previousHeight = -999;
                        }
                    }

                    if (platforms.Count % 3 == 0)
                    {
                        if (Random.value < 0.66f)
                            previousHeight = -999;
                    }

                    
                }
                

                startPoint += new Vector3(0,size.endpointRelative,size.width);
                latestOffset = new Vector3(0,size.endpointRelative,size.width);
                
            }

            tries++;
            if(tries >maxTries)
                succesfull = true;
        }


    }

    IEnumerator roadGenerator(List<GameObject> inactivePool , List<GameObject> activePool){

        while(true){

            if(activePool.Count < workingLength)
                PickPiece(inactivePool,activePool);


            yield return new WaitForSeconds(0.05f);
        }


    }

    public void removeFromPool(GameObject obj){
        if (workingCase == 1)
        {
            if (currentPieces.Contains(obj))
            {
                currentPieces.Remove(obj);
                inactivePool.Add(obj);
                obj.GetComponent<MovePiece>().isRunning = false;
                obj.SetActive(false);
                gameManager.PiecesPassed = 1;
                Debug.Log("Piece passed: " + gameManager.PiecesPassed);
            }
            else
            {
                Destroy(obj);
            }
        }
        else
        {
            if (activeBossFightPieces.Contains(obj))
            {
                activeBossFightPieces.Remove(obj);
                inactiveBossPieces.Add(obj);
                obj.GetComponent<MovePiece>().isRunning = false;
                obj.SetActive(false);
                gameManager.PiecesPassed = 1;
                Debug.Log("Piece passed: " + gameManager.PiecesPassed);
            }
            else
            {
                Destroy(obj);
            }
        }
    }
    


    

    

   
}
