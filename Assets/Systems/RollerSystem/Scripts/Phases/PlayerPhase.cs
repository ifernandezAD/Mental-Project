using UnityEngine;
using UnityEngine.UI;

public class PlayerPhase : Phase
{
    [SerializeField] Button okButton;

    protected override void InternalOnEnable()
    {
        base.InternalOnEnable();
        OKButton.onOKButtonPressed += StartNextPhaseWithDelay;
        Health.onBossDefeated += StartNextActWithDelay;
    }

    protected override void BeginPhase()
    {
        okButton.interactable = true;
        BubblesManager.instance.InstantiateBubbles();
    }

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        OKButton.onOKButtonPressed -= StartNextPhaseWithDelay;
        Health.onBossDefeated -= StartNextActWithDelay;

        okButton.interactable = false;
        BubblesManager.instance.DestroyAllBubbles();
    }
}
