using UnityEngine;

public class RabbitCombat : CombatBehaviour
{
    [Range(0, 100)]
    public float dodgeChance = 25f;

    public override void Defense(int damage)
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= dodgeChance) { return; }

        base.Defense(damage);
    }
}