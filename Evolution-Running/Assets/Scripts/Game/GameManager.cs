using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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
    public GameObject UIScore;

    public AudioSwitcher switcher;
    private int progressStatus;
    private TextMeshProUGUI UIScoreText;
    public float score;
    private float lastPos;
    
    
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
        lastPos = 0;
        enemySpawner.offset = new Vector3(0, 30, 30);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level1")
        {
            score = 0;
        }
        else
        {
            score = readScore();
        }
        
        StartCoroutine(TutorialScreen());

    }

    IEnumerator TutorialScreen()
    {
        
        yield return new WaitForSeconds(7f);
        
        
        UI.SetActive(true);
        UIScore.SetActive(true);
        UIScoreText = UIScore.GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateScore());
        yield return new WaitForSeconds(0.5f);
        UIProgressBar.SetActive(true);
        progressBar.SetMaxProgress(progressTrigger);
        progressBar.SetProgress(0);
        gameplayCamera.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
        player.transform.position = new Vector3(0, 30, 10);
        PlayerMovement.instance.canAttack = true;
        PlayerMovement.instance.canDash = true;
        PlayerManager.instace.currentHP = PlayerManager.instace.maxHP;
        PowerManager.instance.StartDataCollection();
        hpMax = PlayerManager.instace.maxHP;
        ActivateBG();
        
        
    }

    void SpawnBoss()
    {
        
        Debug.Log("Spwaning BOss");
        IncomingText(false);
        bossHelthBar.SetActive(true);
        Boss.SetActive(true);
        UIProgressBar.SetActive(false);
        PieceGenerator.instance.startPoint = Vector3.zero;
        PlayerManager.instace.transform.position = new Vector3(0, 35, 10);
        PieceGenerator.instance.SwitchMode(0);
        enemySpawner.offset = new Vector3(0, 20, 15);
        switcher.SwitchClips();

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
        writeScore(score+50);
        bossHelthBar.SetActive(false);
        UIProgressBar.SetActive(true);
        PieceGenerator.instance.startPoint = Vector3.zero;
        PlayerManager.instace.transform.position = new Vector3(0, 35, 10);
        PieceGenerator.instance.SwitchMode(1);
        progressStatus = 0;
        progressBar.SetProgress(progressStatus);
        Boss.SetActive(false);
        enemySpawner.offset = new Vector3(0, 30, 30);
        //score += 50;
        switcher.SwitchClips();
        
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Level1")
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        if(scene.name == "Level2")
            SceneManager.LoadScene("Level3", LoadSceneMode.Single);
        if (scene.name == "Level3")
            StartCoroutine(TutorialScreen());
        //SceneManager.LoadScene("Level3", LoadSceneMode.Single);
    }

    public void OnDeath()
    {
        gameplayCamera.SetActive(false);
        bossHelthBar.SetActive(false);
        progressStatus = 0;
        //progressBar.SetProgress(progressStatus);
        Boss.SetActive(false);
        UIBossText.SetActive(false);
        UI.SetActive(false);
        //PlayerMovement.instance.Start();
        player.SetActive(false);
        PieceGenerator.instance.startPoint = Vector3.zero;
        PieceGenerator.instance.SwitchMode(1);
        writeScore(0);
        
        Start();
    }

    // Update is called once per frame
    IEnumerator UpdateScore()
    {
        while (true)
        {
            if (player.transform.position.z > lastPos)
                score += (player.transform.position.z - lastPos) / 5;
            lastPos = player.transform.position.z;
            if (UIScore.activeInHierarchy)
                UIScoreText.text = "Score : " + score.ToString(".00");
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ActivateBG()
    {
        //backGround.gameObject.SetActive(true);
    }

    public void writeScore(float score)
    {

        StreamWriter writer = new StreamWriter(Application.dataPath + "\\scoreValue.txt");
        writer.Write(score);
        writer.Flush();
        writer.Close();
    }

    public float readScore()
    {
        string text = System.IO.File.ReadAllText(Application.dataPath + "\\scoreValue.txt");
        if (text != "")
        {
            float value = float.Parse(text, CultureInfo.InvariantCulture.NumberFormat);
            return value;
        }
        else
        {
            return 0;
        }
    }
}
