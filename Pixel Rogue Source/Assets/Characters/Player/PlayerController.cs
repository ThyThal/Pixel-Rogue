using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerController : MonoBehaviour
{
    [Header("Main Stats")] // Variables.
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth;
    [SerializeField] public HealthBar healthBar;

    [Header("Movement")]
    [SerializeField] public float Speed;
    [SerializeField] public float originalSpeed;
    [SerializeField] public float Jump;

    [Header("Combat")] 
    [SerializeField] public bool isBlocking;
    [SerializeField] public int weaponDamage;
    [SerializeField] public int originalDamage;
    [SerializeField] public bool isHurt;
    [SerializeField] public bool buffShield;
    [SerializeField] public bool hitGolem;

    [Header("Blind Effect")]
    [SerializeField] private bool blinded;
    [SerializeField] private Color blindLight;
    [SerializeField] private float blindTimer;
    [SerializeField] private float currentBlindTimer;

    [Header("Timers")]
    [SerializeField] private float hurtTime;
    [SerializeField] private float currentHurtTime;
    [SerializeField] private float blockTimer = 0.5f;
    [SerializeField] private float currentBlockTimer;

    [Header("Components")] // Components
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerBuff playerBuff;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private Transform bloodPosition;
    [SerializeField] public UnityEngine.Experimental.Rendering.Universal.Light2D playerLight;
    [SerializeField] public SpriteRenderer playerRenderer;

    [SerializeField] public float originalViewDistance;
    
    [Header("Audio")] // Audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip dieAudio;
    [SerializeField] private AudioClip angelAudio;
    [SerializeField] private AudioClip blockAudio;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        playerBuff = GetComponent<PlayerBuff>();
        currentHealth = maxHealth;
        healthBar.MaxHealth(maxHealth);
        animator = GetComponentInChildren<Animator>();
        playerLight = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        playerRenderer = GetComponentInChildren<SpriteRenderer>();
        originalViewDistance = playerLight.pointLightOuterRadius;
        
    }

    public void TakeDamage(int damage)
    {
        if (isBlocking)
        {
            audioSource.PlayOneShot(blockAudio);
            return;
        }
        
        if (buffShield)
        {
            buffShield = false;
            healthBar.pShield.SetActive(false);
            audioSource.PlayOneShot(angelAudio);
            playerRenderer.color = Color.white;
            return;
        }
        
        if (!isBlocking)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        
            isHurt = true;
            Instantiate(bloodEffect, bloodPosition.position, Quaternion.identity);
            if (currentHealth > 0)
            {
                audioSource.PlayOneShot(hurtAudio);
            }
        
            if (currentHealth <= 0)
            {
                audioSource.PlayOneShot(dieAudio);
                animator.SetBool("isDead", true);
                GetComponent<PlayerAttack>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
                gameManager.GameOver();
            }
        }
    }

    public void Block()
    {
        isBlocking = true;
        currentBlockTimer = 0;
        animator.SetBool("Defend", false);
    }

    public void Blind()
    {
        blinded = true;
        if (isBlocking)
        {
            playerLight.pointLightOuterRadius = 7.5f;
        }

        if (!isBlocking)
        {
            playerLight.pointLightOuterRadius = 4f;
        }
        playerLight.color = new Color(0.57f, 1f, 0.56f);
    }

    public void Unblind()
    {
        blinded = false;
        currentBlindTimer = 0f;

        if (playerBuff.pDamage)
        {
            playerLight.color = new Color(1f, 0.47f, 0.47f);
        }

        if (playerBuff.pSpeed)
        {
            playerLight.pointLightOuterRadius = 10f;
        }
        
        if (playerBuff.pSpeed == false)
        {
            playerLight.pointLightOuterRadius = 7.5f;
        }
        
        if (playerBuff.pDamage == false)
        {
            playerLight.color = Color.white;
        }
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentHealth = 1000;
            currentHealth = 1000;
            weaponDamage = 1000;
            originalDamage = 1000;
            Speed = 8;
            originalSpeed = 8;
        }

        #endif
        if (blinded)
        {
            if (currentBlindTimer < blindTimer)
            {
                currentBlindTimer += Time.deltaTime;
            }

            else if (currentBlindTimer >= blockTimer)
            {
                Unblind();
            }
        }
        
        if (isBlocking)
        {
            if (currentBlockTimer < blockTimer)
            {
                currentBlockTimer += Time.deltaTime;
            }

            else if (currentBlockTimer >= blockTimer)
            {
                isBlocking = false;
            }
        }
        
        if (isHurt)
        {
            if (currentHurtTime < hurtTime)
            {
                currentHurtTime += Time.deltaTime;
            }

            if (currentHurtTime >= hurtTime)
            {
                isHurt = false;
                currentHurtTime = 0;
            }
        }
    }
}
