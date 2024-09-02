using UnityEngine;

public class TransformEmptySlotsSkill : Skill
{
   public override void TriggerSkill()
    {
        BubblesManager.instance.TransformEmptyBubblesToDefenseOrAttack();

        base.TriggerSkill();
    }
}
