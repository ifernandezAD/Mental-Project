using UnityEngine;

public class ReviveHalfLifeRelic : Relic
{
    protected override void Effect()
    {
        Health health = GameLogic.instance.mainCharacterCard.GetComponent<Health>();
        health.EnableReviveWithHalfLife();
    }
}
