using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime;
    [SerializeField] public string sceneName;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.gameReset)
            {
                Debug.Log("game reset is true");
                GameManager.Instance.sceneTransition = this;
                GameManager.Instance.gameReset = false;
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*private void Update()
    {
        if (GameManager.Instance.gameReset)
        {
            Debug.Log("game reset is true");
            GameManager.Instance.sceneTransition = this;
            GameManager.Instance.gameReset = false;
        }
    }*/

    public void loadScene()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel() // <====={ WAIT FOR LOAD}
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}