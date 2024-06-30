using System;
using UnityEngine;

public class CardDetector : MonoBehaviour
{   
    private Card card;
    private Health health;

    void Awake()
    {
        card = GetComponent<CardDisplay>().card;
        health=GetComponent<Health>();
    }

    public void CheckButtonType(DraggableButton button)
    {
        if(button.gameObject.tag == "Sword")
        {
           if(card.cardType == Card.CardType.Enemy)
           {
                health.ChangeLives(-1);
           }
        }

        Debug.Log("Button released over the card!");
        Destroy(button.gameObject);
    }

  
}