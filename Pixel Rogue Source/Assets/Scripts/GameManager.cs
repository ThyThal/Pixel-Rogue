using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Variables")]
    [SerializeField] public int points;
    [SerializeField] public bool playerDead;
    [SerializeField] private bool gameFinished;
    [SerializeField] public bool gameReset;
    [SerializeField] public int currentWave;

    [Header("Game References")]
    [SerializeField] public SceneTransition sceneTransition;
    [SerializeField] public Text pointsText;
    [SerializeField] public Text waveText;
    
    [Header("Scenes")]
    [SerializeField] public string gameScene;
    [SerializeField] public string winScene;
    [SerializeField] public string loseScene;
    [SerializeField] public string mainScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P)) // <==========[AUTOMATIC WIN] 
        {
            Win();
        }
        
        if (Input.GetKeyDown(KeyCode.O)) // <==========[AUTOMATIC LOSE] 
        {
            GameOver();
        }
        
        #endif
        if (!gameFinished)
        {
            if (points == 5)
            {
                //Win();
            }

            if (playerDead)
            {
                gameFinished = true;
                GameOver();
            }
        }
    }
    
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        pointsText.text = $"Score: {points}";
        //Debug.Log("Score: " + points);
    }

    public void UpdateWave()
    {
        waveText.text = $"Wave {currentWave}";
    }
    
    public void GameOver() // <====={ SCENE LOSE }
    {
        sceneTransition.sceneName = loseScene;
        sceneTransition.loadScene();
        playerDead = true;
        gameFinished = true;
    }

    public void Win() // <====={ SCENE WIN }
    {
        sceneTransition.sceneName = winScene;
        sceneTransition.loadScene();
        gameFinished = true;
    }

    public void Restart() // <====={ SCENE GAME }
    {
        //Debug.Log("gameReset: Game Manager Restart");
        sceneTransition.sceneName = gameScene;
        sceneTransition.loadScene();
        playerDead = false;
        gameFinished = false;
        points = 0;
        //Debug.Log("restart");
    }

    public void MainMenu()
    {
        sceneTransition.sceneName = mainScene;
        sceneTransition.loadScene();
        playerDead = false;
        gameFinished = false;
        points = 0;
    }
}