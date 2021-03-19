using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    
    private void Start()
    {
        Invoke("Destroy", lifeTime);
    }
    
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
