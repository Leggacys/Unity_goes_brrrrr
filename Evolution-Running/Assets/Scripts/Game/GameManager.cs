using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    
    public float hpMax;
    public GameObject Boss;
    public int progressTrigger;
    public GameObject bossHelthBar;
    public ProgressBar progressBar;
    public GameObject UIProgressBar;
    private int progressStatus;
    
    
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
    
    
    public int PiecesPassed
    {
        get
        {
            return progressStatus;
        }

        set
        {
            
            progressStatus += value;
            if(UIProgressBar.activeSelf)
                progressBar.SetProgress(progressStatus);
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
        progressBar.SetMaxProgress(progressTrigger);
    }

    void SpawnBoss()
    {
        bossHelthBar.SetActive(true);
        Boss.SetActive(true);
        UIProgressBar.SetActive(false);
        PieceGenerator.instance.startPoint = Vector3.zero;
        PlayerManager.instace.transform.position = new Vector3(0, 15, 10);
        PieceGenerator.instance.SwitchMode(0);
        
    }

    public void OnBossDefeat()
    {
        bossHelthBar.SetActive(false);
        UIProgressBar.SetActive(true);
        PieceGenerator.instance.startPoint = Vector3.zero;
        PlayerManager.instace.transform.position = new Vector3(0, 20, 10);
        PieceGenerator.instance.SwitchMode(1);
        progressStatus = 0;
        progressBar.SetProgress(progressStatus);
        Boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
