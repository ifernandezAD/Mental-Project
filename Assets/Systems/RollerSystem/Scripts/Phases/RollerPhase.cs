using UnityEngine;
using UnityEngine.UI;

public class RollerPhase : Phase
{   
    [SerializeField] Button rollButton;

    protected override void InternalOnEnable()
    {
        base.InternalOnEnable();
        OKButton.onOKButtonPressed += StartNextPhaseWithDelay;
    }
    protected override void BeginPhase()
    {
        rollButton.interactable=true;
        Roller.instance.ResetRoller();
    }

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        OKButton.onOKButtonPressed -= StartNextPhaseWithDelay;
    }


}
