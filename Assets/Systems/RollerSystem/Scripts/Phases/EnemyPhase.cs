using System;
using UnityEngine;
using DG.Tweening;

public class EnemyPhase : Phase
{
    private Health characterHealth;

    protected override void BeginPhase()
    {
        characterHealth = characterCardContainer.GetChild(0).GetComponent<Health>();

        if (enemyContainerFront.childCount == 0 && enemyContainerUp.childCount == 0 && enemyContainerDown.childCount == 0)
        {
            UIManagement.instance.CloseCurtain();
            DOVirtual.DelayedCall(1, () => { StartNextPhaseWithDelay(); });
            return;
        }

        bool hasBoss = HasBossCard(enemyContainerFront) || HasBossCard(enemyContainerUp) || HasBossCard(enemyContainerDown);

        ManageMentalHealthDamageApplied(() =>
        {
            if (hasBoss)
            {
                DOVirtual.DelayedCall(0.5f, () => { StartRollerPhaseWithDelay(); });
            }
            else
            {
                UIManagement.instance.CloseCurtain();
                DOVirtual.DelayedCall(1, () => { StartNextPhaseWithDelay(); });
            }
        });
    }

    private void ManageMentalHealthDamageApplied(Action onComplete)
    {
        Sequence attackSequence = DOTween.Sequence();

        AddContainerAttacksToSequence(attackSequence, enemyContainerFront);
        AddContainerAttacksToSequence(attackSequence, enemyContainerUp);
        AddContainerAttacksToSequence(attackSequence, enemyContainerDown);

        attackSequence.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    private void AddContainerAttacksToSequence(Sequence sequence, Transform container)
    {
        foreach (Transform enemy in container)
        {
            CombatBehaviour combatBehaviour = enemy.GetComponent<CombatBehaviour>();
            if (combatBehaviour != null)
            {
                sequence.AppendCallback(() =>
                {
                    combatBehaviour.Attack();
                });

                sequence.AppendInterval(1f);
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
