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
        int enemyDamage = enemyCardContainer.GetChild(0).GetComponent<CardDisplay>().card.attack;

        characterCardContainer.GetChild(0).GetComponent<Health>().RemoveHealth(enemyDamage);
    }

}
