using UnityEngine;

public class TransformEmptySlotsSkill : Skill
{
   public override void TriggerSkill()
    {
        BubblesManager.instance.TransformBubblesToRandom();

        base.TriggerSkill();
    }
}
