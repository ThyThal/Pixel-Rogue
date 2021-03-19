using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")] // Variables
    [SerializeField] private float stepTime = 0.41f;
    [SerializeField] private float currentStepTime;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float timerHurt;
    [SerializeField] public int fallDamage;

    [Header("Components")] // Components
    [SerializeField] private PlayerBuff playerBuff;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] public Rigidbody2D myRigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider2D;
    
    [Header("Audio")] // Audio
    [SerializeField] private AudioSource moveSource;

    [SerializeField] private AudioClip pickAudio;
    [SerializeField] private AudioClip stepAudio;
    [SerializeField] private AudioClip landAudio;
    [SerializeField] private AudioSource pickSource;
    [SerializeField] private AudioSource buffSource;
    [SerializeField] public AudioClip buffAudio;
    [SerializeField] public AudioClip healAudio;
    
    // Animation ID.
    private static readonly int Velocity = Animator.StringToHash("Velocity");
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerBuff = GetComponent<PlayerBuff>();
        playerAttack = GetComponent<PlayerAttack>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        collider2D = GetComponent<Collider2D>();
    }
    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H)) // <====={ DEBUG TAKE DAMAGE } 
        {
            playerController.TakeDamage(50);
        }

        #endif
        currentStepTime += Time.deltaTime;
        if (!playerController.isHurt && !playerController.isBlocking)
        {
            if (Input.GetKey(KeyCode.LeftShift) && isGrounded) // <====={ RUN } 
            {
                currentSpeed = playerController.Speed + 0.5f;
                animator.speed = 1.5f;
                stepTime = 0.31f;
            }

            else // <====={ RESET SPEED & ANIMATION TIMES & STOP } 
            {
                currentSpeed = playerController.Speed;
                stepTime = 0.41f;
                animator.speed = 1f;
                Stop();
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !playerAttack.isAttacking) // <====={ JUMP } 
            {
                myRigidbody.AddForce(Vector2.up * playerController.Jump, ForceMode2D.Impulse);
                isGrounded = false;
                animator.SetBool("Jump", true);
            }

            else if (Input.GetKey(KeyCode.A) && !playerAttack.isAttacking) // <====={ MOVE LEFT }
            {
                Move();
                transform.rotation = Quaternion.Euler(0, 180, 0);

                if ((currentStepTime >= stepTime) && isGrounded)
                {
                    moveSource.PlayOneShot(stepAudio);
                    currentStepTime = 0;
                }
            }

            else if (Input.GetKey(KeyCode.D) && !playerAttack.isAttacking) // <====={ MOVE RIGHT }
            {
                Move();
                transform.rotation = Quaternion.Euler(0, 0, 0);

                if ((currentStepTime >= stepTime) && isGrounded)
                {
                    moveSource.PlayOneShot(stepAudio);
                    currentStepTime = 0;
                }
            }
            animator.SetFloat(Velocity, Mathf.Abs(myRigidbody.velocity.x));
        }
    }
    
    private void Move() // <====={ MOVE PLAYER }
    {
        myRigidbody.velocity = new Vector2(transform.right.x * currentSpeed, myRigidbody.velocity.y);
    }
    
    private void Stop() // <====={ STOP PLAYER MOVEMENT }
    {
        if (playerController.isHurt)
        {
            timerHurt -= Time.deltaTime;
            if (timerHurt <= 0)
            {
                playerController.isHurt = false;
            }
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        }
    }

    public void Knockback(int knockback)
    {
        myRigidbody.AddForce(Vector2.up * knockback, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p_life pLife = collision.gameObject.GetComponent<p_life>(); // <====={ DETECT LIFE COLLISION }
        if (pLife != null)
        {
            pLife.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        p_life pLife = collision.gameObject.GetComponent<p_life>(); // <====={ DETECT LIFE COLLISION }
        if (pLife != null)
        {
            if (playerController.currentHealth < playerController.maxHealth)
            {
                buffSource.PlayOneShot(healAudio);
                playerController.Heal(50);
                pLife.Pick();
                if (playerController.currentHealth > playerController.maxHealth)
                    playerController.currentHealth = playerController.maxHealth;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) // // <====={ DETECT COLLISIONS }
    {
        p_coin pCoin = collision.gameObject.GetComponent<p_coin>(); // <====={ DETECT COIN COLLISION }
        if (pCoin != null)
        {
            pickSource.PlayOneShot(pickAudio);
            pCoin.Pick();
            return;
        }
        
        p_damage pDamage = collision.gameObject.GetComponent<p_damage>(); // <====={ DETECT DAMAGE COLLISION }
        if (pDamage != null)
        {
            buffSource.PlayOneShot(buffAudio);
            playerBuff.BuffDamage();
            pDamage.Pick();
            return;
        }
        
        p_speed pSpeed = collision.gameObject.GetComponent<p_speed>(); // <====={ DETECT SPEED COLLISION }
        if (pSpeed != null)
        {
            buffSource.PlayOneShot(buffAudio);
            playerBuff.BuffSpeed();
            pSpeed.Pick();
            return;
        }
        
        p_shield pShield = collision.gameObject.GetComponent<p_shield>(); // <====={ DETECT SHIELD     COLLISION }
        if (pShield != null)
        {
            buffSource.PlayOneShot(buffAudio);
            playerBuff.BuffShield();
            pShield.Pick();
            return;
        }
        
        if (collision.gameObject.CompareTag("Ground")) // <====={ DETECT GROUND COLLISION }
        {
            moveSource.PlayOneShot(landAudio);
            isGrounded = true;
            animator.SetBool(Jump, false);
            if (playerController.hitGolem)
            {
                playerController.TakeDamage(fallDamage);
                playerController.hitGolem = false;
            }
        }
    }
}
