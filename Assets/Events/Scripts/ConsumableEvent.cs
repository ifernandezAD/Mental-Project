using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableEvent : GenericEvent
{
    [SerializeField] ConsumableObject[] consumablesPool;
    [SerializeField] GameObject[] consumablePrefabs;
    [SerializeField] GameObject[] consumableDummies;
    [SerializeField] ConsumableObject consumableBad;
    [SerializeField] GameObject consumableBadPrefab;
    private Transform objectContainer;

    private void Awake()
    {
        objectContainer = GameLogic.instance.objectContainer;
    }

    public override void Initialize(bool isFlashback)
    {
        base.Initialize(isFlashback);
        SelectRandomConsumableObjects();
    }

    private void OnDisable()
    {
        ActivateDummies(false);
    }

    private void ActivateDummies(bool isActive, int activeCount = 0)
    {
        for (int i = 0; i < consumableDummies.Length; i++)
        {
            consumableDummies[i].SetActive(isActive && i < activeCount);
        }
    }

    private void SelectRandomConsumableObjects()
    {
        if (IsFlashback && !IsGoodFlashback) 
        {
            SetBadDummyConsumable(consumableDummies[0], consumableBad);
            ActivateDummies(true, 1); 
            return;
        }

        int consumablesToSelect = (IsFlashback && IsGoodFlashback) ? 3 : 1;

        if (consumablesPool.Length < consumablesToSelect)
        {
            Debug.LogWarning("No hay suficientes consumibles en el pool.");
            return;
        }

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < consumablesPool.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < consumablesToSelect; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            int selectedConsumableIndex = availableIndices[randomIndex];

            SetDummyConsumable(consumableDummies[i], consumablesPool[selectedConsumableIndex]);

            availableIndices.RemoveAt(randomIndex);
        }

        ActivateDummies(true, consumablesToSelect);
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

    private void SetBadDummyConsumable(GameObject dummy, ConsumableObject badConsumable)
    {
        ConsumableDisplay consumableDisplay = dummy.GetComponent<ConsumableDisplay>();

        consumableDisplay.consumableObject = badConsumable;
        consumableDisplay.consumableArt.sprite = badConsumable.consumableArt;

        Button dummyButton = dummy.GetComponent<Button>();
        dummyButton.onClick.RemoveAllListeners();
        dummyButton.onClick.AddListener(() => OnConsumableClick(badConsumable));
    }

    public void OnConsumableClick(ConsumableObject consumable)
    {
        GameObject selectedPrefab;

        if (consumable == consumableBad)
        {
            selectedPrefab = consumableBadPrefab;
        }
        else
        {
            int consumableIndex = consumable.index;

            if (consumableIndex >= 0 && consumableIndex < consumablePrefabs.Length)
            {
                selectedPrefab = consumablePrefabs[consumableIndex];
            }
            else
            {
                Debug.LogWarning("Índice de consumible fuera de rango o inválido.");
                return;
            }
        }

        Instantiate(selectedPrefab, objectContainer);
        Debug.Log("Consumable instantiated: " + selectedPrefab.name);
    }
}
