using UnityEngine;

public class DoubleResilienceSkill : Skill
{
    public override void TriggerSkill()
    {
        BubblesManager.instance.TransformBubblesToDoubleResilience();
        base.TriggerSkill();
    }
}
