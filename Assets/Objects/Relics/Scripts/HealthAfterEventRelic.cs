using System;
using Unity.VisualScripting;
using UnityEngine;

public class HealthAfterEventRelic : Relic
{
    [SerializeField] int amountToHeal = 1;

    void OnEnable()
    {
        DrawPhase.onEventTriggered += AddHealthToPlayer;
    }

    private void AddHealthToPlayer()
    {
        Health health = GameLogic.instance.mainCharacterCard.GetComponent<Health>();
        health.AddHealth(amountToHeal);
    }

    void OnDisable()
    {
        DrawPhase.onEventTriggered -= AddHealthToPlayer;
    }

}
