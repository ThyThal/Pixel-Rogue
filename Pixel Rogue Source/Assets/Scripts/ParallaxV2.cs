using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxV2 : MonoBehaviour
{
    // Variables
    [SerializeField] private Vector3 lastCameraPosition;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float width;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        width = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        var deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffect, 0f, 0f);
        lastCameraPosition = cameraTransform.position;
        
        /*
         //<====={ INFINITE SCROLL PARALLAX }=====>
         var distanceCamera = cameraTransform.position.x - transform.position.x;
         
         if (Mathf.Abs(distanceCamera) >= width)
        {
            var movement = distanceCamera > 0 ? width * 2f : width * -2f;
            transform.position = new Vector3(transform.position.x + movement, transform.position.y, 0f);
        }
        */
    }
}
