using UnityEngine;
using System;

public class AllyEvent : EventManager
{
    [SerializeField] Card[] alliesPool; // Mismo orden que el Array de aliados del GameLogic
    [SerializeField] GameObject allyTemplate; 
    private int selectedAllyIndex; 
    public static Action<int> onAllyObtained; 

    private void Start()
    {
        selectedAllyIndex = UnityEngine.Random.Range(0, alliesPool.Length);

        SetAllyCard(alliesPool[selectedAllyIndex]);

        allyTemplate.SetActive(true);
    }

    public void AllyObtained()
    {
        onAllyObtained?.Invoke(selectedAllyIndex);
    }

    private void SetAllyCard(Card selectedCard)
    {
        CardDisplay cardDisplay = allyTemplate.GetComponent<CardDisplay>();

        cardDisplay.card = selectedCard;

        cardDisplay.cardArt.sprite = selectedCard.art;
        cardDisplay.healthText.text = selectedCard.maxHealth.ToString();
        cardDisplay.staminaText.text = 0.ToString();
    }


}
