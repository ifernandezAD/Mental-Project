using System;
using System.Collections.Generic;
using UnityEngine;

public class AllyEvent : MonoBehaviour
{
    [SerializeField] Card[] alliesPool; 
    [SerializeField] GameObject[] allyArray; 
    [SerializeField] GameObject allyTemplate; 
    [SerializeField] Transform allyContainer; 

    private int selectedAllyIndex; 
    private List<GameObject> remainingAllies; 



    private void Awake()
    {
        // Inicializa la lista de aliados restantes usando el array de aliados
        remainingAllies = new List<GameObject>(allyArray);
    }

    private void Start()
    {
        selectedAllyIndex = UnityEngine.Random.Range(0, alliesPool.Length);
        SetAllyCard(alliesPool[selectedAllyIndex]);
        allyTemplate.SetActive(true);
    }

    public void AllyObtained()
    {
        InstantiateAlly(selectedAllyIndex); 
    }

    private void InstantiateAlly(int allyIndex)
    {
        if (allyIndex >= 0 && allyIndex < remainingAllies.Count)
        {
            GameObject selectedAlly = remainingAllies[allyIndex];
            GameObject newAlly = Instantiate(selectedAlly, allyContainer);

            remainingAllies.RemoveAt(allyIndex); 
        }
        else
        {
            Debug.LogWarning("Índice de aliado fuera de rango o inválido.");
        }
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
