using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : MonoBehaviour
{
    // Variables.
    [Header("Combat")]
    [SerializeField] private int weaponDamage;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float currentCooldown;

    [Header("Variables")]
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField, Range(0, 2)] private float attackArea;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float distance;
    [SerializeField] private RaycastHit2D hitInfo;

    // Componentes.
    [SerializeField] private Animator animator;
    [SerializeField] private WolfMovement wolfMovement;
    [SerializeField] private WolfController wolfController;

    // Audio
    [SerializeField] private AudioSource attackAudio;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        wolfMovement = GetComponent<WolfMovement>();
        wolfController = GetComponent<WolfController>();
        currentCooldown = cooldownTime;
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
            wolfController.isAttacking = false;
            hitInfo = Physics2D.Raycast(attackPoint.position, transform.right, distance, enemyLayers);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player") && !wolfController.isAttacking)
                {
                    animator.SetTrigger("Attack");
                    currentCooldown = 0;
                    wolfController.isAttacking = true;
                    wolfMovement.Stop();
                    attackAudio.Play();
                }
            }
        }
    }

    public void Attack()
    {
        //Debug.Log("Choco con el jugador");
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