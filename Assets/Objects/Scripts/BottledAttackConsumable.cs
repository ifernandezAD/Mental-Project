using UnityEngine;

public class BottledAttackConsumable : Consumable
{

    public override void Use()
    {
        BubblesManager.instance.AddDoubleSwordBubble();
    }
}
