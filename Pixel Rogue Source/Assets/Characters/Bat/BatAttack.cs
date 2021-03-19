using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] public int damage;
    [SerializeField] private float cooldownAttack;
    [SerializeField] private float cooldownCurrent;

    [Header("Variables")] 
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private RaycastHit2D hitInfo;
    [SerializeField] private float distance;
    
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private BatMovement batMovement;
    [SerializeField] private BatController batController;
    [SerializeField] private AudioSource attackAudio;

    private void Awake()
    {
        cooldownCurrent = cooldownAttack;
        animator = GetComponentInChildren<Animator>();
        batMovement = GetComponent<BatMovement>();
        batController = GetComponent<BatController>();
    }

    private void Update()
    {
        Debug.DrawRay(attackPoint.position, transform.right, Color.green);
        
        if (cooldownCurrent < cooldownAttack)
        {
            cooldownCurrent += Time.deltaTime;
        }
        
        if (cooldownCurrent >= cooldownAttack)
        {
            batController.canAttack = true;
            batController.isAttacking = false;
            hitInfo = Physics2D.Raycast(attackPoint.position, transform.right, distance, enemyLayers);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player") && !batController.isAttacking)
                {
                    animator.SetTrigger("Attack");
                    cooldownCurrent = 0;
                    batController.isAttacking = true;
                    batMovement.Stop();
                    attackAudio.Play();
                }
            }
        }
    }
    
    public void Attack()
    {
        hitInfo.collider.GetComponent<PlayerController>().TakeDamage(damage);
        batController.canAttack = false;
    }

}
