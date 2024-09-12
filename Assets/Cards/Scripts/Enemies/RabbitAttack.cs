using UnityEngine;

public class RabbitAttack : AttackBehaviour
{
    [Range(0, 100)]
    public float dodgeChance = 25f;

    public override void Attack()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= dodgeChance) { return; }

        base.Attack();
    }
}