using UnityEngine;

public class GoatTailCombat : CombatBehaviour
{
    public override void Attack() 
    {
        AnimateAttack(() =>
        {
            StatsManager.instance.ApplyDamageToRandomTargetNoResilience(enemyDamage);

        });
    }
}
