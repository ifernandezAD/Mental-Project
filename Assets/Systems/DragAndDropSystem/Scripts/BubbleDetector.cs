using System;
using UnityEngine;

public class BubbleDetector : MonoBehaviour
{
    private Card card;
    private Health health;
    private Skill skill;
    private CombatBehaviour combatBehaviour;

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

    public void CheckButtonType(DraggableButton button)
    {
        if (button.gameObject.tag == "Sword")
        {
            if (card.cardType == Card.CardType.Enemy)
            {
                combatBehaviour.Defense(1);
                Destroy(button.gameObject);
            }
        }


        if (button.gameObject.tag == "Heart")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                health.AddResilience(1);
                Destroy(button.gameObject);
            }
        }


        if (button.gameObject.tag == "Book")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                skill.IncreaseStamina(1);
                Destroy(button.gameObject);
            }
        }

        if (button.gameObject.tag == "DoubleRay")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                skill.IncreaseStamina(2);
                Destroy(button.gameObject);
            }
        }

        if (button.gameObject.tag == "DoubleShield")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                health.AddResilience(2);
                Destroy(button.gameObject);
            }
        }

        if (button.gameObject.tag == "DoubleSword")
        {
            if (card.cardType == Card.CardType.Enemy)
            {
                combatBehaviour.Defense(2);
                Destroy(button.gameObject);
            }
        }

    }


}