using UnityEngine;

public class BottledStaminaConsumable : Consumable
{
    public override void Use()
    {
        BubblesManager.instance.AddDoubleStaminaBubble();
    }
}
