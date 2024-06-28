using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPhase : Phase
{
    protected override void BeginPhase()
    {
        //TODO Aquí irá la lógica de quitar vida uno a uno y de uso de habilidades, quizás activar botones o cartas
        ManageEnemyCardDamageTaken();

        StartNextPhaseWithDelay();
    }

    private void ManageEnemyCardDamageTaken()
    {
        int damageTaken = Roller.instance.GetImageCount(ImageType.Sword);

        enemyCardContainer.GetChild(0).GetComponent<EnemyHealth>().ChangeLives(damageTaken); //Cachear
    }

}
