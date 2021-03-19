using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WitchShoot : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float distance = 0.1f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private bool hitTarget;
    [SerializeField] private float waitDestroy = 0.5f;

    private void Start()
    {
        Invoke("Destroy", lifeTime);
        spriteRender = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, distance, enemyLayers);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player") && !hitTarget)
            {
                hitInfo.collider.GetComponent<PlayerController>().TakeDamage(damage);
                hitInfo.collider.GetComponent<PlayerController>().Blind();
                spriteRender.color = Color.clear;
                hitTarget = true;
                audioSource.PlayOneShot(hitAudio);
                //Debug.Log("Damaged");
            }

            if (waitDestroy <= 0)
            {
                Destroy();
            }
            
            else if (waitDestroy > 0)
            {
                waitDestroy -= Time.deltaTime;
            }
        }
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    private void Destroy()
    {
        audioSource.PlayOneShot(hitAudio);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy();
        }
    }
}
