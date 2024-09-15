using UnityEngine;

public class BottledResilienceConsumable : Consumable
{
    public override void Use()
    {
        BubblesManager.instance.AddDoubleResilienceBubble();
    }
}
