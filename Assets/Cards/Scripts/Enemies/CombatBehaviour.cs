using TMPro;
using UnityEngine;

public class CombatBehaviour : MonoBehaviour
{
    protected CardDisplay cardDisplay;
    protected Health health;
    [SerializeField] protected TextMeshProUGUI attackText;
    protected int enemyDamage;

    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
        health = GetComponent<Health>();
        enemyDamage = cardDisplay.card.attack;
    }
    public virtual void Attack()
    {

        StatsManager.instance.ApplyDamageToRandomTarget(enemyDamage);
    }

    public virtual void Defense(int damage)
    {
        health.RemoveHealth(damage);
    }

    public void DebuffAttack()
    {
        enemyDamage--;

        if (enemyDamage <= 0)
        {
            enemyDamage = 0;
        }

        attackText.text=enemyDamage.ToString();
    }

    protected void BuffAttack()
    {
        enemyDamage++;
        attackText.text=enemyDamage.ToString();
    }
}
