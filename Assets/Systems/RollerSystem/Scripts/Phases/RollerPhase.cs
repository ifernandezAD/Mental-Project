using UnityEngine;
using UnityEngine.UI;

public class RollerPhase : Phase
{   
    [SerializeField] Button rollButton;
    protected override void BeginPhase()
    {
        rollButton.interactable=true;
        Roller.instance.ResetRoller();

        this.enabled=false;
    }
}
