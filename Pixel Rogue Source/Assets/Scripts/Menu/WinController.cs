using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    // Variables
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private SceneTransition sceneTransition;
    private void Awake()
    {
        restartButton.onClick.AddListener(OnClickRestart);
        mainButton.onClick.AddListener(OnClickMainMenu);
        sceneTransition = GetComponentInChildren<SceneTransition>();
    }
    
    private void OnClickRestart()
    {
        GameManager.Instance.sceneTransition = sceneTransition;
        GameManager.Instance.gameReset = true;
        GameManager.Instance.Restart();
    }
    
    private void OnClickMainMenu()
    {
        GameManager.Instance.sceneTransition = sceneTransition;
        GameManager.Instance.gameReset = true;
        GameManager.Instance.MainMenu();
    }
}
