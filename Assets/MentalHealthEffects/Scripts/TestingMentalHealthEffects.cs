using UnityEngine;

public class TestingMentalHealthEffects : MentalHealthEffects
{
    public override void TriggerFirstActMentalHealthEffects()
    {
        Debug.Log("First mental health effects added to the game");
    }

    public override void TriggerSecondActMentalHealthEffects()
    {
       Debug.Log("Second mental health effects added to the game");
    }
    public override void TriggerThirdActMentalHealthEffects()
    {
        Debug.Log("Third mental health effects added to the game");
    }
}
