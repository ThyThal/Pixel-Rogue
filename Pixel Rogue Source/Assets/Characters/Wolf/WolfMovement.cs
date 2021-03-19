using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    [Header("Components")] // Components
    [SerializeField] private WolfController wolfController;
    [SerializeField] private Animator animator;

    [Header("Variables")] // Variables
    [SerializeField] private float stepTime;
    [SerializeField] private float originalStepTime;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float currentStepTime;
    [SerializeField] private float timerHurt;
    [SerializeField] private float timerAttack;
    [SerializeField] private float ogtimerHurt;
    [SerializeField] private float ogtimerAttack;
    [SerializeField] private float checkDistance;
    
    [SerializeField] public float distance;

    [Header("Audio")] // Audio
    [SerializeField] private AudioSource moveSource;
    [SerializeField] private AudioClip stepAudio;
    
    private static readonly int Run = Animator.StringToHash("Run");

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        wolfController = GetComponent<WolfController>();
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        target = playerObject.transform;
        originalStepTime = stepTime;
        ogtimerHurt = timerHurt;
        ogtimerAttack = timerAttack;
    }

    private void Update()
    {
        stepTime -= Time.deltaTime;
        if (wolfController.isHurt || wolfController.isAttacking)
        {
            animator.SetBool(Run, false);
            Stop();
        }

        if (!wolfController.isHurt && !wolfController.isAttacking)
        {
            animator.SetBool(Run, true);
            Move();
            
            if (stepTime <= 0)
            {
                moveSource.PlayOneShot(stepAudio);
                stepTime = originalStepTime;
            }
        }
    }

    private void Move() // <======{ MOVE WOLF }
    {
        // Get direction to transform.
        var directionTarget = target.position - transform.position;
        distance = directionTarget.magnitude;

        // Rotate Sprite.
        transform.rotation = target.position.x - transform.position.x > 0
            ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

        // Move to target.
        var move = transform.right * (speed * Time.deltaTime);
        transform.position += Vector3.ClampMagnitude(move, distance);
    }

    public void Stop() // <======{ STOP MOVING WOLF }
    {
        stepTime = 0;
        if (wolfController.isHurt) // <======{ WOLF IS HURT }
        {
            timerHurt -= Time.deltaTime;
            if (timerHurt <= 0)
            {
                wolfController.isHurt = false;
                timerHurt = ogtimerHurt;
            }
        }

        else if (wolfController.isAttacking) // // <======{ WOLF IS ATTACKING }
        {
            timerAttack -= Time.deltaTime;
            if (timerAttack <= 0)
            {
                wolfController.isAttacking = false;
                timerAttack = ogtimerAttack;
            }
        }
    }
}