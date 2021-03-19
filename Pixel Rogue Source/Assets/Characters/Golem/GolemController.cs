using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GolemController : MonoBehaviour
{
    [Header("Main Stats")] // Variables
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private int lootNumber;

    [Header("Combat")]
    [SerializeField] public bool isHurt;
    [SerializeField] public bool isAttacking;
    [SerializeField] private Transform lootPos;

    [Header("Components")] // Components
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject pLoot;
    [SerializeField] private GameObject CoinLoot;

    [Header("Audio")] // Audio
    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip dieAudio;
    
    [Header("Loot")] // Loot
    [SerializeField] private GameObject pHeal;
    [SerializeField] private GameObject pDamage;
    [SerializeField] private GameObject pShield;
    [SerializeField] private GameObject pSpeed;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        isHurt = true;
        currentHealth -= damage;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);

        if (currentHealth > 0) // <====={ GOLEM DAMAGE }
        {
            damageAudio.PlayOneShot(hurtAudio);
        }

        if (currentHealth <= 0) // <====={ GOLEM DIE }
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
        enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<GolemMovement>().enabled = false;
        GetComponent<GolemAttack>().enabled = false;
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