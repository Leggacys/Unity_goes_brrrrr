using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    
    public float hpMax;
    public GameObject Boss;
    public GameObject player;
    public int progressTrigger;
    public GameObject bossHelthBar;
    public ProgressBar progressBar;
    public GameObject UIProgressBar;
    public GameObject UIBossText;
    public GameObject UI;
    public GameObject gameplayCamera;
    public EnemySpawn enemySpawner;
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
            
            
            if (UIProgressBar.activeInHierarchy)
            {
                progressStatus += value;
                progressBar.SetProgress(progressStatus);
                if (progressStatus == progressTrigger)
                {
                    SpawnBoss();
                }
                else
                {
                    if(progressTrigger - progressStatus<=2)
                        IncomingText(true);
                }
                
                
                
            }
            
               
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        enemySpawner.offset = new Vector3(0, 30, 30);
        
        StartCoroutine(TutorialScreen());

    }

    IEnumerator TutorialScreen()
    {
        
        yield return new WaitForSeconds(7f);
        
        
        UI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        progressBar.SetMaxProgress(progressTrigger);
        gameplayCamera.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
        player.transform.position = new Vector3(0, 30, 10);
        PlayerManager.instace.currentHP = PlayerManager.instace.maxHP;
        PowerManager.instance.StartDataCollection();
        hpMax = PlayerManager.instace.maxHP;
        
        
    }

    void SpawnBoss()
    {
        
        Debug.Log("Spwaning BOss");
        IncomingText(false);
        bossHelthBar.SetActive(true);
        Boss.SetActive(true);
        UIProgressBar.SetActive(false);
        PieceGenerator.instance.startPoint = Vector3.zero;
        PlayerManager.instace.transform.position = new Vector3(0, 30, 10);
        PieceGenerator.instance.SwitchMode(0);
        enemySpawner.offset = new Vector3(0, 20, 15);

    }

    void IncomingText(bool status)
    {
        Debug.Log("Status: " + status);

        if (status != UIBossText.activeInHierarchy)
        {
            UIBossText.SetActive(status);
        }
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
        enemySpawner.offset = new Vector3(0, 30, 30);
    }

    public void OnDeath()
    {
        gameplayCamera.SetActive(false);
        UI.SetActive(false);
        player.SetActive(false);
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
