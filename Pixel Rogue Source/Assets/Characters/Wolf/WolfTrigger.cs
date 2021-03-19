using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTrigger : MonoBehaviour
{
    [SerializeField] private WolfAttack wolfAttack;
    [SerializeField] private WolfController wolfController;

    private void Start()
    {
        wolfAttack = GetComponentInParent<WolfAttack>();
        wolfController = GetComponentInParent<WolfController>();
    }

    public void TriggerAttack()
    {
        wolfAttack.Attack();
    }

    public void TriggerDestroy()
    {
        wolfController.Destroy();
    }

    public void TriggerDead()
    {
        wolfController.TriggerDie();
    }
}
