using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float safeDistance;

    [Header("Movement")] 
    [SerializeField] private Vector3 directionToTarget;
    [SerializeField] private Vector3 directionToMe;
    [SerializeField] private Vector3 directionToSafe;
    [SerializeField] private Vector3 normalizedToMe;
    
    [SerializeField] private float distanceToTarget;
    [SerializeField] private float distanceToMe;
    [SerializeField] private Vector3 movement;
    
    [Header("Timers")]
    [SerializeField] private float timerAttack;
    [SerializeField] private float timerHurt;
    [SerializeField] private float ogtimerAttack;
    [SerializeField] private float ogtimerHurt;

    [Header("Components")]
    [SerializeField] private BatController batController;
    [SerializeField] private BatAttack batAttack;

    private void Start()
    {
        batController = GetComponent<BatController>();
        batAttack = GetComponent<BatAttack>();
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        target = playerObject.transform;
        ogtimerHurt = timerHurt;
        ogtimerAttack = timerAttack;
    }

    private void Update()
    {
        if (batController.canAttack)
        {
            MoveAttack();
        }

        else
        {
            MoveSafe();
        }
    }

    private void MoveAttack()
    {
        directionToTarget = target.transform.position - transform.position;
        distanceToTarget = directionToTarget.magnitude;
        directionToTarget = directionToTarget.normalized;
        
        movement = directionToTarget * (speed * Time.deltaTime);
        transform.position += Vector3.ClampMagnitude(movement, distanceToTarget);
        Debug.DrawRay(transform.position, directionToTarget, Color.green);
    }

    private void MoveSafe()
    {
        directionToMe = transform.position - target.transform.position;
        normalizedToMe = directionToMe.normalized;
        directionToSafe = target.position + (normalizedToMe * safeDistance);

        var direction = directionToSafe - transform.position;
        var distanceSafe = direction.magnitude;
        var dirNormalized = direction.normalized;
        var movementDir = dirNormalized;
        
        movement = movementDir * (speed * Time.deltaTime);
        transform.position += Vector3.ClampMagnitude(movement, distanceSafe);
        
        
        
        
        Debug.DrawRay(transform.position, direction, Color.green );
    }

    public void Stop() // <======{ STOP MOVING ENEMY }
    {
        if (batController.isHurt) // <======{ ENEMY IS HURT }
        {
            timerHurt -= Time.deltaTime;
            if (timerHurt <= 0)
            {
                batController.isHurt = false;
                timerHurt = ogtimerHurt;
            }
        }

        else if (batController.isAttacking) // // <======{ ENEMY IS ATTACKING }
        {
            timerAttack -= Time.deltaTime;
            if (timerAttack <= 0)
            {
                batController.isAttacking = false;
                timerAttack = ogtimerAttack;
            }
        }
    }
}
