using UnityEngine;

public class SharkAttack : AttackBehaviour
{
    public override void Attack()
    {
        base.Attack();
        cardDisplay.card.attack++;
    }
}
