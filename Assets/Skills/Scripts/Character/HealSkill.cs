using UnityEngine;

public class HealSkill : Skill
{
    public override void TriggerSkill()
    {
        health.AddHealth(1);
        base.TriggerSkill();
    }
}
