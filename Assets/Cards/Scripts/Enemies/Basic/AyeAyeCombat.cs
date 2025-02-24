using UnityEngine;

public class AyeAyeCombat : CombatBehaviour
{
    [SerializeField] int staminaToDecrease = 1;

    public override void Attack()
    {
        StatsManager.instance.ReduceStaminaForAll(staminaToDecrease);
        base.Attack();
    }
}
