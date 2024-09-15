using UnityEngine;

public class GainResilienceOnLostHealthRelic : Relic
{
    protected override void Effect()
    {
        Health health = GameLogic.instance.mainCharacterCard.GetComponent<Health>();
        health.EnableResilienceOnLostHealth();
    }
}
