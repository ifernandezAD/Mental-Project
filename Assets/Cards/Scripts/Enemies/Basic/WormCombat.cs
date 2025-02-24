using UnityEngine;

public class WormCombat : CombatBehaviour
{
    public override void Attack()
    {
        StatsManager.instance.ApplyDamageToRandomTargetNoResilience(enemyDamage);
    }
}
