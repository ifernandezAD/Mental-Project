using UnityEngine;

public class WormCombat : CombatBehaviour
{
    public override void Attack()
    {
        int enemyDamage = cardDisplay.card.attack;
        StatsManager.instance.ApplyDamageToRandomTargetNoResilience(enemyDamage);
    }
}
