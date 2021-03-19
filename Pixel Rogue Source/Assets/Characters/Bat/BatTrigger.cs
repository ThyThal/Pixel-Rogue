using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTrigger : MonoBehaviour
{
    [SerializeField] private BatAttack batAttack;
    [SerializeField] private BatController batController;

    private void Start()
    {
        batAttack = GetComponentInParent<BatAttack>();
        batController = GetComponentInParent<BatController>();
    }

    public void TriggerAttack()
    {
        batAttack.Attack();
    }

    public void TriggerDestroy()
    {
        batController.Destroy();
    }
    
    public void TriggerDead()
    {
        batController.TriggerDie();
    }
}
