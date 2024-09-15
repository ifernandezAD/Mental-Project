using UnityEngine;

public class SurviveLetalHitRelic : Relic
{
    protected override void Effect()
    {
        Health health = GameLogic.instance.mainCharacterCard.GetComponent<Health>();
        health.EnableLethalSurvival();
    }
}
