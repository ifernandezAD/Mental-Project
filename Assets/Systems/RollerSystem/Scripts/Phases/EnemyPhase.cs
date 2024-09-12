using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPhase : Phase
{
    private Health characterHealth;

    protected override void BeginPhase()
    {
        characterHealth = characterCardContainer.GetChild(0).GetComponent<Health>();

        if (enemyCardContainer.childCount == 0)
        {
            StartNextPhaseWithDelay();
            return;
        }

        ManageMentalHealthDamageApplied();

        foreach (Transform child in enemyCardContainer)
        {
            CardDisplay cardDisplay = child.GetComponent<CardDisplay>();
            if (cardDisplay != null && cardDisplay.card.isBoss)
            {
                StartRollerPhaseWithDelay();
                return;
            }
        }

        StartNextPhaseWithDelay();
    }

    private void ManageMentalHealthDamageApplied()
    {
        for (int i = 0; i < enemyCardContainer.childCount; i++)
        {
            CombatBehaviour combatBehaviour = enemyCardContainer.GetChild(i).GetComponent<CombatBehaviour>();
            if (combatBehaviour != null)
            {
                combatBehaviour.Attack();
            }
        }
    }
}
