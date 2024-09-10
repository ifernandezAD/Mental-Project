using System;
using UnityEngine;

public class BubbleDetector : MonoBehaviour
{   
    private Card card;
    private Health health;
    private Skill skill;

    void Awake()
    {
        card = GetComponent<CardDisplay>().card;
        health=GetComponent<Health>();
        skill=GetComponent<Skill>();
    }

    public void CheckButtonType(DraggableButton button)
    {
        if(button.gameObject.tag == "Sword")
        {
           if(card.cardType == Card.CardType.Enemy)
           {
                health.RemoveHealth(1);
                Destroy(button.gameObject);
           }
        }


        if(button.gameObject.tag == "Heart")
        {
            if((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                health.AddResilience(1);
                Destroy(button.gameObject);
            }
        }


        if(button.gameObject.tag == "Book")
        {
            if((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                skill.DecreaseStamina(1);
                Destroy(button.gameObject);
            }
        }    
    }

  
}