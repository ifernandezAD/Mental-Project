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
        int enemyDamage = enemyCardContainer.GetChild(0).GetComponent<EnemyCardDisplay>().enemyCard.attack;
        int characterResilience = Roller.instance.GetImageCount(ImageType.Heart);

        int netDamage = Mathf.Max(enemyDamage - characterResilience, 0);

        characterCardContainer.GetChild(0).GetComponent<CharacterMentalHealth>().ChangeMentalHealth(enemyDamage);

    }

}
