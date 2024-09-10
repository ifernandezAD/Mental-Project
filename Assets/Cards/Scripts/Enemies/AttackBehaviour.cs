using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    protected CardDisplay cardDisplay;

    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
    }
    public virtual void Attack()
    {
        int enemyDamage = cardDisplay.card.attack;
        AttackManager.instance.ApplyDamageToRandomTarget(enemyDamage);
    }
}
