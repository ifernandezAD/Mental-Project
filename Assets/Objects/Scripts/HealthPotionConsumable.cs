using UnityEngine;

public class HealthPotionConsumable : Consumable
{
    [SerializeField] int healthAmount = 2;
    public override void Use()
    {
        GameLogic.instance.mainCharacterCard.GetComponent<Health>().AddHealth(healthAmount);
    }
}
