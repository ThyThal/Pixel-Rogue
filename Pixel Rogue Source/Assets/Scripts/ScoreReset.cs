using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreReset : MonoBehaviour
{
    [SerializeField] public Text scoreObject;

    private void Awake()
    {
        scoreObject = GetComponent<Text>();
    }

    void Start()
    {
        if (GameManager.Instance.gameReset == true)
        {
            GameManager.Instance.pointsText = scoreObject;
            //scoreObject.text = "Score: 0";
            GameManager.Instance.gameReset = false;
        }
    }
}
