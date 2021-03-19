using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemTrigger : MonoBehaviour
{
    [SerializeField] private GolemAttack golemAttack;
    [SerializeField] private GolemController golemController;

    private void Start()
    {
        golemAttack = GetComponentInParent<GolemAttack>();
        golemController = GetComponentInParent<GolemController>();
    }

    public void TriggerAttack()
    {
        golemAttack.Attack();
    }

    public void TriggerDestroy()
    {
        golemController.Destroy();
    }
    
    public void TriggerDead()
    {
        golemController.TriggerDie();
    }
}
