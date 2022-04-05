using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    
    public float hpMax;
    public GameObject Boss;
    public int progressTrigger;
    private float progressStatus;
    
    #region Singletone

    public static GameManager instance;
    
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
    
    
    public float PiecesPassed
    {
        get
        {
            return progressStatus;
        }

        set
        {
            
            progressStatus += value;
            if (progressStatus == progressTrigger)
            {
                SpawnBoss();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hpMax = PlayerManager.instace.maxHP;
    }

    void SpawnBoss()
    {
        Boss.SetActive(true);
        PieceGenerator.instance.startPoint = Vector3.zero;
        PlayerManager.instace.transform.position = new Vector3(0, 15, 3);
        PieceGenerator.instance.SwitchMode(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
