using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    [Header("Active Buffs")]
    [SerializeField] public bool pDamage;
    [SerializeField] public bool pShield;
    [SerializeField] public bool pSpeed;
    [SerializeField] public bool pToggle;

    [Header("Buffs Duration")]
    [SerializeField] private float pDamageTime;
    [SerializeField] private float pShieldTime;
    [SerializeField] private float pSpeedTime;

    [SerializeField] private float damageTimer;
    [SerializeField] private float shieldTimer;
    [SerializeField] private float speedTimer;

    [Header("Buff Colors")]
    [SerializeField] private Color damageColor;

    [Header("Components")]
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (pDamage)
        {
            if (pToggle)
            {
                BuffDamage();
                pToggle = false;
            }
            
            if (damageTimer < pDamageTime) // <====={ While Buff is active }
            {
                damageTimer += Time.deltaTime;
            }
            
            if (damageTimer >= pDamageTime)
            {
                playerController.healthBar.pDamage.SetActive(false);
                playerController.weaponDamage = playerController.originalDamage;
                playerController.playerLight.color = Color.white;
                damageTimer = 0;
                pDamage = false;
            }
        }
        
        if (pSpeed)
        {
            if (pToggle)
            {
                BuffSpeed();
                pToggle = false;
            }
            
            if (speedTimer < pSpeedTime)
            {
                speedTimer += Time.deltaTime;
            }
            
            if (speedTimer >= pSpeedTime)
            {
                playerController.healthBar.pSpeed.SetActive(false);
                playerController.Speed = playerController.originalSpeed;
                playerController.playerLight.pointLightOuterRadius = playerController.originalViewDistance;
                speedTimer = 0;
                pSpeed = false;
            }
        }

        if (pShield)
        {
            BuffShield();
            pToggle = false;
            pShield = false;
        }
    }

    public void BuffDamage()
    {
        playerController.healthBar.pDamage.SetActive(true);
        pDamage = true;
        pToggle = true;
        playerController.playerLight.color = new Color(1f, 0.47f, 0.47f);
        playerController.weaponDamage = playerController.originalDamage + 50;
    }

    public void BuffShield()
    {
        playerController.healthBar.pShield.SetActive(true);
        pShield = true;
        pToggle = true;
        playerController.playerRenderer.color = Color.cyan;
        playerController.buffShield = true;
    }

    public void BuffSpeed()
    {
        playerController.healthBar.pSpeed.SetActive(true);
        pSpeed = true;
        pToggle = true;
        playerController.Speed = playerController.originalSpeed + 1;
        playerController.playerLight.pointLightOuterRadius = playerController.originalViewDistance + 2.5f;
    }
}
