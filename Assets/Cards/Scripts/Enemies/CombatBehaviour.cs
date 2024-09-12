using UnityEngine;

public class CombatBehaviour : MonoBehaviour
{
    protected CardDisplay cardDisplay;
    protected Health health;

    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
        health = GetComponent<Health>();
    }
    public virtual void Attack()
    {
        int enemyDamage = cardDisplay.card.attack;
        AttackManager.instance.ApplyDamageToRandomTarget(enemyDamage);
    }

    public virtual void Defense(int damage)
    {
        health.RemoveHealth(damage);
    }
}
