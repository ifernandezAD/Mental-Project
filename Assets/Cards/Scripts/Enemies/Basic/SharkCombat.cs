using UnityEngine;
using TMPro;
public class SharkCombat : CombatBehaviour
{
    public override void Attack()
    {    
        StatsManager.instance.ApplyDamageToRandomTarget(enemyDamage);
        BuffAttack();
    }
}
