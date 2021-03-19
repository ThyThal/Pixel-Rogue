using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    [Header("Components")] // Components
    [SerializeField] private GolemController golemController;
    [SerializeField] private Animator animator;

    [Header("Variables")] // Variables
    [SerializeField] private float stepTime;
    [SerializeField] private float originalStepTime;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float timerHurt;
    [SerializeField] private float timerAttack;
    [SerializeField] public float distance;
    [SerializeField] private float checkDistance;

    [Header("Audio")] // Audio
    [SerializeField] private AudioSource moveSource;
    [SerializeField] private AudioClip stepAudio;
    
    private static readonly int Run = Animator.StringToHash("Run");

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        golemController = GetComponent<GolemController>();
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        target = playerObject.transform;
        originalStepTime = stepTime;
        timerHurt = 0.25f;
        timerAttack = 0.8f;
    }

    private void Update()
    {
        stepTime -= Time.deltaTime;
        if (golemController.isHurt || golemController.isAttacking)
        {
            animator.SetBool(Run, false);
            Stop();
        }

        if (!golemController.isHurt && !golemController.isAttacking)
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

    private void Move() // <======{ MOVE GOLEM }
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

    public void Stop() // <======{ STOP MOVING GOLEM }
    {
        stepTime = 0;
        if (golemController.isHurt) // <======{ GOLEM IS HURT }
        {
            timerHurt -= Time.deltaTime;
            if (timerHurt <= 0)
            {
                golemController.isHurt = false;
                timerHurt = 0.25f;
            }
        }

        else if (golemController.isAttacking) // // <======{ GOLEM IS ATTACKING }
        {
            timerAttack -= Time.deltaTime;
            if (timerAttack <= 0)
            {
                golemController.isAttacking = false;
                timerAttack = 0.8f;
            }
        }
    }
}