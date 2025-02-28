using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPhase : Phase
{
    public static PlayerPhase instance;
    [SerializeField] Button okButton;
    public static Action onPlayerPhaseEnded;
    public static Action onPlayerPhaseBegin;

    [SerializeField] int bossPartsDefeatedCount = 0;

    protected override void InternalAwake()
    {
        base.InternalAwake();
        instance=this;
    }

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
            okButton.interactable = true; 
            okButton.GetComponent<OKButton>().ActivateBossDefeated();
        }
    }

    public void ResetBossPartsCount()
    {
        bossPartsDefeatedCount=0;
    }

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        OKButton.onOKButtonPressed -= StartNextPhaseWithDelay;
        Health.onBossDefeated -= CountBossPartDefeat;

        okButton.interactable = false;
        BubblesManager.instance.DestroyAllBubbles();
        Slots.instance.UnlockAllDamageLocks();
        onPlayerPhaseEnded?.Invoke();
    }
}
