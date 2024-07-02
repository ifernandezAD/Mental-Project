using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPhase : Phase
{
    protected override void BeginPhase()
    {
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


            characterCardContainer.GetChild(0).GetComponent<Health>().RemoveHealth(enemyDamage);
        }
    }
}
