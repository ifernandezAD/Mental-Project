using System;
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
