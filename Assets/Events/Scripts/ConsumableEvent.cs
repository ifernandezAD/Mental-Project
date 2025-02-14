using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableEvent : GenericEvent
{
    [SerializeField] ConsumableObject[] consumablesPool;
    [SerializeField] GameObject[] consumablePrefabs;
    [SerializeField] GameObject[] consumableDummies;
    private Transform objectContainer;

    private void Awake()
    {
        objectContainer = GameLogic.instance.objectContainer;
    }

    private void OnEnable()
    {
         if (IsFlashback)
        {
            Debug.Log("Este evento proviene de un flashback.");
        }

        ActivateDummies(true);
        SelectRandomConsumableObjects();
    }

    private void OnDisable()
    {
        ActivateDummies(false);
    }

    private void ActivateDummies(bool isActive)
    {
        foreach (var dummy in consumableDummies)
        {
            dummy.SetActive(isActive);
        }
    }

    private void SelectRandomConsumableObjects()
    {
        if (consumablesPool.Length < consumableDummies.Length)
        {
            Debug.LogWarning("No hay suficientes consumibles en el pool.");
            return;
        }

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < consumablesPool.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < consumableDummies.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            int selectedConsumableIndex = availableIndices[randomIndex];

            SetDummyConsumable(consumableDummies[i], consumablesPool[selectedConsumableIndex]);

            availableIndices.RemoveAt(randomIndex);
        }
    }


    private void SetDummyConsumable(GameObject dummy, ConsumableObject consumable)
    {
        ConsumableDisplay consumableDisplay = dummy.GetComponent<ConsumableDisplay>();

        consumableDisplay.consumableObject = consumable;
        consumableDisplay.consumableArt.sprite = consumable.consumableArt;

        Button dummyButton = dummy.GetComponent<Button>();
        dummyButton.onClick.RemoveAllListeners(); 
        dummyButton.onClick.AddListener(() => OnConsumableClick(consumable)); 
    }

    public void OnConsumableClick(ConsumableObject consumable)
    {
        
        int consumableIndex = consumable.index;

        
        if (consumableIndex >= 0 && consumableIndex < consumablePrefabs.Length)
        {
            GameObject selectedPrefab = consumablePrefabs[consumableIndex];
            Instantiate(selectedPrefab, objectContainer);

            Debug.Log("Consumable instantiated: " + selectedPrefab.name);
        }
        else
        {
            Debug.LogWarning("Índice de consumible fuera de rango o inválido.");
        }
    }

}
