using System;
using UnityEngine;
using DG.Tweening;

public class EnemyPhase : Phase
{
    private Health characterHealth;

    protected override void BeginPhase()
    {
        characterHealth = characterCardContainer.GetChild(0).GetComponent<Health>();

        if (enemyContainerFront.childCount == 0 && enemyContainerUp.childCount == 0 && enemyContainerDown.childCount==0)
        {
            UIManagement.instance.CloseCurtain();
            DOVirtual.DelayedCall(1, () =>{StartNextPhaseWithDelay();});    
            return;
        }

        ManageMentalHealthDamageApplied();

        if (HasBossCard(enemyContainerFront) || HasBossCard(enemyContainerUp)||HasBossCard(enemyContainerDown))
        {
            Debug.Log("Boss new round started");
            StartRollerPhaseWithDelay();
            return;
        }

        UIManagement.instance.CloseCurtain();
        DOVirtual.DelayedCall(1, () =>{StartNextPhaseWithDelay();}); 
    }

    private void ManageMentalHealthDamageApplied()
    {
        ApplyAttacksFromContainer(enemyContainerFront);

        ApplyAttacksFromContainer(enemyContainerUp);
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
