using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Components
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private Animator helpAnimator;
    
    // Variables
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private string gameScene;
    [SerializeField] private float timeShowHelp = 3f;
    [SerializeField] private float timer;
    [SerializeField] private bool isHelp;
    
    // Buttons
    [SerializeField] private Button playButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [Header("Audio")] // Audio.
    [SerializeField] private AudioSource talkSource;
    [SerializeField] private AudioClip showAudio;
    [SerializeField] private AudioClip hideAudio;
    
    private static readonly int Show = Animator.StringToHash("Show");

    private void Awake()
    {
        sceneTransition = GetComponentInChildren<SceneTransition>();
        helpAnimator = helpMenu.GetComponent<Animator>();
        playButton.onClick.AddListener(OnClickPlay);
        helpButton.onClick.AddListener(OnClickHelp);
        exitButton.onClick.AddListener(OnClickExit);
        backButton.onClick.AddListener(OnClickBack);
    }

    private void OnClickPlay()
    {
        sceneTransition.sceneName = gameScene;
        sceneTransition.loadScene();
    }
    private void OnClickHelp()
    {
        isHelp = true;
        helpAnimator.SetBool(Show, true);
        talkSource.PlayOneShot(showAudio);
        
    }
    private void OnClickExit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        
        #endif
        Application.Quit();
    }

    private void OnClickBack()
    {
        Debug.Log("sdfgasgasdghads");
        helpAnimator.SetBool(Show, false);
        isHelp = false;
        talkSource.PlayOneShot(hideAudio);
    }

    public void MainMenuHide()
    {
        mainMenu.SetActive(false);
    }
    
    public void MainMenuShow()
    {
        mainMenu.SetActive(true);
    }
}
