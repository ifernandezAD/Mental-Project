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
        
        Slots.instance.UnlockAllNormalSlots();
        Energy.instance.ResetEnergy();
        Roller.instance.DisableAllSlotImages();
    }

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        OKButton.onOKButtonPressed -= StartNextPhaseWithDelay;
    }


}
