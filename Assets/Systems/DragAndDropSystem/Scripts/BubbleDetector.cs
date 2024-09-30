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

    public void CheckButtonType(DraggableButton button, int multiplier)
    {
        if (button.gameObject.tag == "Sword")
        {
            if (card.cardType == Card.CardType.Enemy)
            {
                combatBehaviour.Defense(multiplier);
                onSwordUsed?.Invoke();
                Destroy(button.gameObject);
            }
        }


        if (button.gameObject.tag == "Heart")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                health.AddResilience(multiplier);
                Destroy(button.gameObject);
            }
        }


        if (button.gameObject.tag == "Book")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                skill.IncreaseStamina(multiplier);
                Destroy(button.gameObject);
            }
        }
        
    }
}