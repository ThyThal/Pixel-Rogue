using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveReset : MonoBehaviour
{
    [SerializeField] public Text waveObject;

    private void Awake()
    {
        waveObject = GetComponent<Text>();
    }

    void Start()
    {
        if (GameManager.Instance.gameReset)
        {
            GameManager.Instance.waveText = waveObject;
        }
    }
}
