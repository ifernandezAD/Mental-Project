using UnityEngine;

public class GoatBodyCombat : CombatBehaviour
{
    public override void Attack()
    {
        base.Attack();
        Energy.instance.ChangeEnergyRemovedStatus(true);
        Energy.instance.ShowDecreasedEnergy();
    }
}
