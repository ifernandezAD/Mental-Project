using UnityEngine;

public class GoatHeadCombat : CombatBehaviour
{

    public override void Attack()
    {
        base.Attack();
        Slots.instance.ApplyDamageLockToRandomSlots();
    }
}
