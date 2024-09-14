using UnityEngine;

public class AntidoteConsumable : Consumable
{

    public override void Use()
    {
        Roller.instance.RemoveAllPoisonSymbols();
    }
}
