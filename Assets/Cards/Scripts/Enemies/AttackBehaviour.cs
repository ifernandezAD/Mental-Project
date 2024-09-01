using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private CardDisplay cardDisplay;

    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
    }
    public virtual void Attack()
    {
        int enemyDamage = cardDisplay.card.attack;
        GameLogic.instance.ApplyDamageToRandomTarget(enemyDamage);
    }
}
