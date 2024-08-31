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

        ManageMentalHealthDamageTaken();

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

    private void ManageMentalHealthDamageTaken()
    {
        for (int i = 0; i < enemyCardContainer.childCount; i++)
        {
            CardDisplay enemyCardDisplay = enemyCardContainer.GetChild(i).GetComponent<CardDisplay>();
            int enemyDamage = enemyCardDisplay.card.attack;

             GameLogic.instance.ApplyDamageToRandomTarget(enemyDamage);
        }
    }
}
