using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPhase : Phase
{
    [SerializeField] Button okButton;
    public static Action onPlayerPhaseEnded;
    public static Action onPlayerPhaseBegin;

    [SerializeField] int bossPartsDefeatedCount = 0;

    protected override void InternalOnEnable()
    {
        base.InternalOnEnable();
        onPlayerPhaseBegin?.Invoke();
        OKButton.onOKButtonPressed += StartNextPhaseWithDelay;
        Health.onBossDefeated += CountBossPartDefeat;
    }

    protected override void BeginPhase()
    {
        okButton.interactable = true;
        BubblesManager.instance.InstantiateBubbles();
    }

    private void CountBossPartDefeat()
    {
        bossPartsDefeatedCount++;

        if (bossPartsDefeatedCount >= 3)
        {
            StartNextActWithDelay();
            bossPartsDefeatedCount = 0;
        }
    }

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        OKButton.onOKButtonPressed -= StartNextPhaseWithDelay;
        Health.onBossDefeated -= StartNextActWithDelay;

        okButton.interactable = false;
        BubblesManager.instance.DestroyAllBubbles();
        onPlayerPhaseEnded?.Invoke();
    }
}
