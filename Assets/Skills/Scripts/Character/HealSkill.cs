using UnityEngine;

public class HealSkill : Skill
{
    [SerializeField] int healAmount = 1;
    public override void TriggerSkill()
    {
        StatsManager.instance.HealAllAlliesAndPlayer(healAmount);
        base.TriggerSkill();
    }
}
