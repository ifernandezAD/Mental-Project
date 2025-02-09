using System.Collections.Generic;
using UnityEngine;

public class DebuffAttackSkill : Skill
{
    public override void TriggerSkill()
    {
        List<GameObject> enemyTargets = StatsManager.instance.GetAllEnemyTargets();

        foreach (GameObject enemy in enemyTargets)
        {
            CombatBehaviour combatBehaviour = enemy.GetComponent<CombatBehaviour>();
            if (combatBehaviour != null)
            {
                combatBehaviour.DebuffAttack(1);
            }
        }
        base.TriggerSkill();
    }
}
