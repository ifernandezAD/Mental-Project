using System;
using UnityEngine;

public class BubbleDetector : MonoBehaviour
{
    private Card card;
    private Health health;
    private Skill skill;
    private CombatBehaviour combatBehaviour;

    [Header("Relics")]
    public static Action onSwordUsed;

    void Awake()
    {
        card = GetComponent<CardDisplay>().card;
        health = GetComponent<Health>();
        skill = GetComponent<Skill>();

        if (card.cardType == Card.CardType.Enemy)
        {
            combatBehaviour = GetComponent<CombatBehaviour>();
        }
    }

    public void CheckButtonType(BubbleBehaviour button, int multiplier)
    {
        if (button.gameObject.tag == "Attack")
        {
            if (card.cardType == Card.CardType.Enemy)
            {
                combatBehaviour.Defense(multiplier);
                onSwordUsed?.Invoke();
                Destroy(button.gameObject);
            }
        }


        if (button.gameObject.tag == "Resilience")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                health.AddResilience(multiplier);
                Destroy(button.gameObject);
            }
        }


        if (button.gameObject.tag == "Stamina")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                skill.IncreaseStamina(multiplier);
                Destroy(button.gameObject);
            }
        }

        if (button.gameObject.tag == "DebuffAttack")
        {
            if (card.cardType == Card.CardType.Enemy)
            {
                combatBehaviour.DebuffAttack(multiplier);
                Destroy(button.gameObject);
            }
        }
    }
}