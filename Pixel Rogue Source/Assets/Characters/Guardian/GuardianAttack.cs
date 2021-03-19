using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAttack : MonoBehaviour
{
    // Variables.
    [Header("Combat")]
    [SerializeField] private int weaponDamage;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float currentCooldown;

    [Header("Variables")]
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField, Range(0,2)] private float attackArea;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float distance;
    [SerializeField] private RaycastHit2D hitInfo;

    // Componentes.
    [SerializeField] private Animator animator;
    [SerializeField] private GuardianMovement guardianMovement;
    [SerializeField] private GuardianController guardianController;

    // Audio
    [SerializeField] private AudioSource attackAudio;

    private void Awake()
    {
        currentCooldown = cooldownTime;
        animator = GetComponentInChildren<Animator>();
        guardianMovement = GetComponent<GuardianMovement>();
        guardianController = GetComponent<GuardianController>();
    }

    private void Update()
    {
        Debug.DrawRay(attackPoint.position, transform.right, Color.green);
        
        if (currentCooldown < cooldownTime)
        {
            currentCooldown += Time.deltaTime;
        }
        
        if (currentCooldown >= cooldownTime)
        {
            guardianController.isAttacking = false;
            hitInfo = Physics2D.Raycast(attackPoint.position, transform.right, distance, enemyLayers);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player") && !guardianController.isAttacking)
                {
                    animator.SetTrigger("Attack");
                    currentCooldown = 0;
                    guardianController.isAttacking = true;
                    guardianMovement.Stop();
                    attackAudio.Play();
                }
            }
        }
    }

    public void Attack()
    {
        hitInfo.collider.GetComponent<PlayerController>().TakeDamage(weaponDamage);
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
