using UnityEngine;

public class ToucanCombat : CombatBehaviour
{
    public override void Attack()
    {
        for (int i = 0; i < 3; i++)
        {
            StatsManager.instance.ApplyDamageToRandomTarget(enemyDamage);
        }
    }
}
