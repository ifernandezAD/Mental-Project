using UnityEngine;

public class BirdMentalHealthEffects : MentalHealthEffects
{
    private DrawPhase drawPhase;

    private void Awake()
    {
        drawPhase = GameLogic.instance.drawPhase;
    }

    public override void TriggerFirstActMentalHealthEffects()
    {
        drawPhase.ChangeProbability(0.25f);
    }

    public override void TriggerSecondActMentalHealthEffects()
    {
        drawPhase.ChangeProbability(0.50f);
    }

    public override void TriggerThirdActMentalHealthEffects()
    {
        drawPhase.ChangeProbability(0.75f);
    }
}
