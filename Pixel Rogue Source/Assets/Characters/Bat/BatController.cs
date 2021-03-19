using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [Header("Main Stats")] // Variables
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int currentHealth;
    
    [Header("Combat")]
    [SerializeField] public bool isHurt;
    [SerializeField] public bool isAttacking;
    [SerializeField] public bool canAttack;
    [SerializeField] private Transform lootPos;
    
    [Header("Components")] // Components
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bloodEffect;

    [Header("Loot")] // Loot
    [SerializeField] private int lootNumber;
    [SerializeField] private GameObject pHeal;
    [SerializeField] private GameObject pDamage;
    [SerializeField] private GameObject pShield;
    [SerializeField] private GameObject pSpeed;
    [SerializeField] private GameObject pLoot;
    [SerializeField] private GameObject CoinLoot;
    
    [Header("Audio")] // Audio
    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip dieAudio;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        canAttack = true;
        ChooseLoot();
    }
    
    public void TakeDamage(int damage)
    {
        isHurt = true;
        currentHealth -= damage;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        
        if (currentHealth > 0) // <====={ ENEMY DAMAGE }
        {
            damageAudio.PlayOneShot(hurtAudio);
        }
        
        if (currentHealth <= 0) // <====={ ENEMY DIE }
        {
            damageAudio.PlayOneShot(dieAudio);
            animator.SetBool("isDead", true);
            Die();
        }
    }
    
    private void ChooseLoot()
    {
        lootNumber = Random.Range(0,100);
        if (lootNumber < 70)
        {
            return;
        }
        
        if (lootNumber >= 70 && lootNumber < 85 )
        {
            pLoot = pHeal;
            return;
        }
        
        if (lootNumber >= 85 && lootNumber < 90 )
        {
            pLoot = pDamage;
            return;
        }
        
        if (lootNumber >= 90 && lootNumber < 95 )
        {
            pLoot = pShield;
            return;
        }
        
        if (lootNumber > 95)
        {
            pLoot = pSpeed;
        }

    }
    
    private void Die()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BatMovement>().enabled = false;
        GetComponent<BatAttack>().enabled = false;
        if (lootNumber >= 70)
        {
            Instantiate(pLoot, lootPos.position, transform.rotation);
        }
        Instantiate(CoinLoot, lootPos.position, transform.rotation);
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void TriggerDie()
    {
        animator.SetBool("Destroy", true);
    }
}

