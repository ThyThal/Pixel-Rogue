using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] public Text scoreObject;

    private void Start()
    {
        var points = GameManager.Instance.points;
        scoreObject = GetComponent<Text>();
        GameManager.Instance.pointsText = scoreObject;
        GameManager.Instance.pointsText.text = $"Score: {points}";
    }    
}