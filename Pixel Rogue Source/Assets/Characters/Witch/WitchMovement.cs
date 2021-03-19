using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    [Header("Components")] // Components
    [SerializeField] private WitchController witchController;
    [SerializeField] private Animator animator;
    
    [Header("Variables")] // Variables
    [SerializeField] private float stepTime;
    [SerializeField] private float originalStepTime;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float timerHurt;
    [SerializeField] private float ogtimerAttack;
    [SerializeField] private float ogtimerHurt;
    [SerializeField] private float timerAttack;
    [SerializeField] public float distance;
    
    [Header("Audio")] // Audio
    [SerializeField] private AudioSource moveSource;
    [SerializeField] private AudioClip stepAudio;
    
    private static readonly int Walk = Animator.StringToHash("Walk");
    
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        witchController = GetComponent<WitchController>();
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        target = playerObject.transform;
        originalStepTime = stepTime;
        ogtimerHurt = timerHurt;
        ogtimerAttack = timerAttack;
    }

    private void Update()
    {
        stepTime -= Time.deltaTime;
        if (witchController.isHurt || witchController.isAttacking)
        {
            animator.SetBool(Walk, false);
            Stop();
        }
        
        if (!witchController.isHurt && !witchController.isAttacking)
        {
            animator.SetBool(Walk, true);
            Move();
            
            if (stepTime <= 0)
            {
                moveSource.PlayOneShot(stepAudio);
                stepTime = originalStepTime;
            }
        }
    }

    private void Move() // <======{ MOVE ENEMY }
    {
        // Get direction to transform.
        var directionTarget = target.position - transform.position;
        distance = directionTarget.magnitude;

        // Rotate Sprite.
        transform.rotation = target.position.x - transform.position.x > 0
            ? Quaternion.Euler(0,0,0) : Quaternion.Euler(0,180,0);

        // Move to target.
        var move = transform.right * (speed * Time.deltaTime);
        transform.position += Vector3.ClampMagnitude(move, distance);
    }

    public void Stop() // <======{ STOP MOVING ENEMY }
    {
        stepTime = 0;
        if (witchController.isHurt) // <======{ ENEMY IS HURT }
        {
            timerHurt -= Time.deltaTime;
            if (timerHurt <= 0)
            {
                witchController.isHurt = false;
                timerHurt = ogtimerHurt;
            }
        }
        
        else if (witchController.isAttacking) // // <======{ ENEMY IS ATTACKING }
        {
            timerAttack -= Time.deltaTime;
            if (timerAttack <= 0)
            {
                witchController.isAttacking = false;
                timerAttack = ogtimerAttack;
            }
        }
    }
}
