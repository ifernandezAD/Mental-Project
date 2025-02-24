using UnityEngine;

public class HedgehogCombat : CombatBehaviour
{
    public override void Defense(int damage)
    {
        base.Defense(damage);        
        GameLogic.instance.mainCharacterCard.GetComponent<Health>().RemoveHealth(1); 
    }
}
