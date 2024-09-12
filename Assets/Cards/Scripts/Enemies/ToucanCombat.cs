using UnityEngine;

public class ToucanCombat : CombatBehaviour
{
    public override void Attack()
    {
        int enemyDamage = cardDisplay.card.attack; 

        for (int i = 0; i < 3; i++)
        {
            StatsManager.instance.ApplyDamageToRandomTarget(enemyDamage);
        }
    }
}
