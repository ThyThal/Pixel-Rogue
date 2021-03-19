using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAttack : MonoBehaviour
{
    // Variables.
    [Header("Combat")]
    [SerializeField] private int weaponDamage;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float currentCooldown;
    
    [Header("Variables")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField, Range(0,2)] private float attackArea;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float distance;
    [SerializeField] private RaycastHit2D hitInfo;

    // Componentes.
    [SerializeField] private Animator animator;
    [SerializeField] private WitchMovement witchMovement;
    [SerializeField] private WitchController witchController;

    // Audio
    [SerializeField] private AudioSource attackAudio;

    private void Awake()
    {
        currentCooldown = cooldownTime;
        animator = GetComponentInChildren<Animator>();
        witchMovement = GetComponent<WitchMovement>();
        witchController = GetComponent<WitchController>();
    }

    private void Update()
    {
        hitInfo = Physics2D.Raycast(shootPoint.position, transform.right, distance, enemyLayers);
        
        if (currentCooldown < cooldownTime)
        {
            currentCooldown += Time.deltaTime;
        }
        
        if (currentCooldown >= cooldownTime)
        {
            witchController.isAttacking = false;
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player") && !witchController.isAttacking)
                {
                    animator.SetTrigger("Attack");
                    currentCooldown = 0;
                    witchController.isAttacking = true;
                    witchMovement.Stop();
                    attackAudio.Play();
                }
            }
        }
    }

    public void Shoot() // <====={ SHOOT ATTACK }
    {
        Instantiate(projectile, shootPoint.position, shootPoint.rotation);;
    }
}
