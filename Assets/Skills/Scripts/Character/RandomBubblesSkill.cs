using UnityEngine;

public class RandomBubblesSkill : Skill
{
   public override void TriggerSkill()
    {
        BubblesManager.instance.TransformBubblesToRandom();

        base.TriggerSkill();
    }
}