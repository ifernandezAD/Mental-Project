using UnityEngine;

public class ExtraRollRelic : Relic
{
    protected override void Effect()
    {
        Energy.instance.ChangeMaxEnergy(1);
    }
}
