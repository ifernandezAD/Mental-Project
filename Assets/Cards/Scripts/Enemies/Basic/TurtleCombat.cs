using UnityEngine;

public class TurtleCombat : CombatBehaviour
{
    public override void Attack()
    {
        base.Attack();
        health.AddHealth(1);
    }
}
