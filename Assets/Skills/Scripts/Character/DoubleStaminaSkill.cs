using UnityEngine;

public class DoubleStaminaSkill : Skill
{
    public override void TriggerSkill()
    {
        BubblesManager.instance.TransformBubblesToDoubleStamina();
        base.TriggerSkill();
    }
}
