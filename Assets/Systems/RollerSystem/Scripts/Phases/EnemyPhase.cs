using System;
using UnityEngine;

public class EnemyPhase : Phase
{
    private Health characterHealth;

    protected override void BeginPhase()
    {
        characterHealth = characterCardContainer.GetChild(0).GetComponent<Health>();

        if (enemyContainerFront.childCount == 0 && enemyContainerBack.childCount == 0)
        {
            StartNextPhaseWithDelay();
            return;
        }

        ManageMentalHealthDamageApplied();

        if (HasBossCard(enemyContainerFront) || HasBossCard(enemyContainerBack))
        {
            StartRollerPhaseWithDelay();
            return;
        }

        StartNextPhaseWithDelay();
    }

    private void ManageMentalHealthDamageApplied()
    {
        ApplyAttacksFromContainer(enemyContainerFront);

        ApplyAttacksFromContainer(enemyContainerBack);
    }

    private void ApplyAttacksFromContainer(Transform container)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            CombatBehaviour combatBehaviour = container.GetChild(i).GetComponent<CombatBehaviour>();
            if (combatBehaviour != null)
            {
                combatBehaviour.Attack();
            }
        }
    }

    private bool HasBossCard(Transform container)
    {
        foreach (Transform child in container)
        {
            CardDisplay cardDisplay = child.GetComponent<CardDisplay>();
            if (cardDisplay != null && cardDisplay.card.isBoss)
            {
                return true;  
            }
        }
        return false;
    }
}
