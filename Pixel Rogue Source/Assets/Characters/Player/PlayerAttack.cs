using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Components
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;

    [Header("Main Combat")]
    [SerializeField] public bool isAttacking;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackTimer;
    
    [SerializeField] public bool isDefending;
    [SerializeField] private float defendCooldown;
    [SerializeField] private float defendTimer;
    
    // Variables
    [Header("Variables")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField, Range(0,1)] private float attackArea;

    // Audio
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackAudio;

    private void Awake()
    {
        defendTimer = defendCooldown;
        attackTimer = attackCooldown;
        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (attackTimer < attackCooldown)
        {
            attackTimer += Time.deltaTime;
        }
        
        if (defendTimer < defendCooldown)
        {
            defendTimer += Time.deltaTime;
        }
        
        if (attackTimer >= attackCooldown)
        {
            isAttacking = false;
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
                isAttacking = true;
                attackTimer = 0;
            }
        }

        if (defendTimer >= defendCooldown)
        {
            isDefending = false;
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetBool("Defend", true);
                isDefending = true;
                defendTimer = 0;
            }
        }
    }

    public void Attack() // <====={ WEAPON ATTACK }
    {
        audioSource.PlayOneShot(attackAudio);
        // Detect Enemys
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackArea, enemyLayers);

        // Damage Enemys
        foreach (Collider2D enemy in hitEnemys)
        {
            {
                if (enemy.CompareTag("Enemy")) // <======{ DAMAGE GUARDIAN }
                {
                    enemy.GetComponent<GuardianController>().TakeDamage(playerController.weaponDamage);
                }

                else if (enemy.CompareTag("Witch")) // <======{ DAMAGE WITCH }
                {
                    enemy.GetComponent<WitchController>().TakeDamage(playerController.weaponDamage);
                }
                
                else if (enemy.CompareTag("Bat")) // <======{ DAMAGE BAT }
                {
                    enemy.GetComponent<BatController>().TakeDamage(playerController.weaponDamage);
                }
                
                else if (enemy.CompareTag("Wolf")) // <======{ DAMAGE WOLF }
                {
                    enemy.GetComponent<WolfController>().TakeDamage(playerController.weaponDamage);
                }
                
                else if (enemy.CompareTag("Golem")) // <======{ DAMAGE GOLEM }
                {
                    enemy.GetComponent<GolemController>().TakeDamage(playerController.weaponDamage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected() // <====={ Draw Attack Area }
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackArea);
    }
}
