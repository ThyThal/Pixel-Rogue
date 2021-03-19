using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    // Variables.
    [Header("Combat")]
    [SerializeField] private int weaponDamage;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float currentCooldown;
    [SerializeField] private int knockback;
    [SerializeField] private int fallDamage;

    [Header("Variables")]
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField, Range(0, 2)] private float attackArea;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float distance;
    [SerializeField] private RaycastHit2D hitInfo;

    // Componentes.
    [SerializeField] private Animator animator;
    [SerializeField] private GolemMovement golemMovement;
    [SerializeField] private GolemController golemController;

    // Audio
    [SerializeField] private AudioSource attackAudio;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        golemMovement = GetComponent<GolemMovement>();
        golemController = GetComponent<GolemController>();
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
            golemController.isAttacking = false;
            hitInfo = Physics2D.Raycast(attackPoint.position, transform.right, distance, enemyLayers);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player") && !golemController.isAttacking)
                {
                    animator.SetTrigger("Attack");
                    currentCooldown = 0;
                    golemController.isAttacking = true;
                    golemMovement.Stop();
                    attackAudio.Play();
                }
            }
        }
    }

    public void Attack()
    {
        hitInfo.collider.GetComponent<PlayerController>().hitGolem = true;
        hitInfo.collider.GetComponent<PlayerMovement>().fallDamage = fallDamage;
        if (hitInfo.collider.GetComponent<PlayerController>().isBlocking)
        {
            hitInfo.collider.GetComponent<PlayerMovement>().Knockback(knockback);
        }

        else
        {
            hitInfo.collider.GetComponent<PlayerController>().TakeDamage(weaponDamage);
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

