using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTrigger : MonoBehaviour
{
    [SerializeField] private GuardianAttack guardianAttack;
    [SerializeField] private GuardianController guardianController;

    private void Start()
    {
        guardianAttack = GetComponentInParent<GuardianAttack>();
        guardianController = GetComponentInParent<GuardianController>();
    }

    public void TriggerAttack()
    {
        guardianAttack.Attack();
    }

    public void TriggerDestroy()
    {
        guardianController.Destroy();
    }

    public void TriggerDead()
    {
        guardianController.TriggerDie();
    }
}
