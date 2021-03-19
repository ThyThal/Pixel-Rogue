using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerMovement playerMovement;
    

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        playerAttack = GetComponentInParent<PlayerAttack>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void TriggerAttack()
    {
        playerAttack.Attack();
    }

    public void TriggerBlock()
    {
        playerController.Block();
    }
}
