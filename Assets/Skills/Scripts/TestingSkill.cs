using UnityEngine;

public class TestingSkill : Skill
{
    public override void TriggerSkill()
    {
        Debug.Log("Testing skill has been enabled");
        base.TriggerSkill();
    }
}
