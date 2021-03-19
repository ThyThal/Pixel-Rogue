using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWave : MonoBehaviour
{
    [SerializeField] public Text waveObject;

    private void Start()
    {
        var currentWave = GameManager.Instance.currentWave;
        waveObject = GetComponent<Text>();
        GameManager.Instance.waveText = waveObject;
        GameManager.Instance.waveText.text = $"Wave {currentWave}";
    }
}
