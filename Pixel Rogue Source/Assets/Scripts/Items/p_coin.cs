using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_coin : MonoBehaviour
{
    [SerializeField] private int points;

    public void Pick()
    {
        GameManager.Instance.AddPoints(points);
        Destroy(gameObject);
    }
}
