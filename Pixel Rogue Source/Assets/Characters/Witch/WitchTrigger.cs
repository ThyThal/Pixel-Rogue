using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTrigger : MonoBehaviour
{
    [SerializeField] private WitchAttack witchAttack;
    [SerializeField] private WitchController witchController;

    private void Start()
    {
        witchAttack = GetComponentInParent<WitchAttack>();
        witchController = GetComponentInParent<WitchController>();
    }

    public void TriggerAttack()
    {
        witchAttack.Shoot();
    }

    public void TriggerDestroy()
    {
        witchController.Destroy();
    }

    public void TriggerDead()
    {
        witchController.TriggerDie();
    }
}
