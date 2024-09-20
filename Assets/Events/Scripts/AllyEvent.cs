using System.Collections.Generic;
using UnityEngine;

public class AllyEvent : MonoBehaviour
{
    [SerializeField] Card[] alliesPool; 
    [SerializeField] GameObject[] allyArray; 
    [SerializeField] GameObject allyTemplate; 
    private Transform allyContainer; 

    private int selectedAllyIndex; 
    private List<int> selectedAllyIndices = new List<int>(); 
    private List<GameObject> remainingAllies; 

    private void Awake()
    {
        remainingAllies = new List<GameObject>(allyArray);
        allyContainer = GameLogic.instance.allyContainer;
    }

    private void OnEnable()
    {
        SelectRandomAllyCard();
        allyTemplate.SetActive(true);
    }

    private void OnDisable()
    {
        allyTemplate.SetActive(false);
    }

    private void SelectRandomAllyCard()
    {
        if (remainingAllies.Count == 0) return; 

        selectedAllyIndex = UnityEngine.Random.Range(0, alliesPool.Length);
        
        while (selectedAllyIndices.Contains(selectedAllyIndex))
        {
            selectedAllyIndex = UnityEngine.Random.Range(0, alliesPool.Length);
        }

        selectedAllyIndices.Add(selectedAllyIndex);
        SetAllyCard(alliesPool[selectedAllyIndex]);
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
            Instantiate(selectedAlly, allyContainer);
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
