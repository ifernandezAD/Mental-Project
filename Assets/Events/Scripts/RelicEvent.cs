using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicEvent : GenericEvent
{
    [SerializeField] RelicObject[] relicsPool;
    [SerializeField] GameObject[] relicsPrefabs;
    [SerializeField] GameObject[] relicsDummies;
    private Transform relicContainer;

    private void Awake()
    {
        relicContainer = GameLogic.instance.relicContainer;
    }

    private void OnEnable()
    {
        ActivateDummies(true);
        SelectRandomConsumableObjects();
    }

    private void OnDisable()
    {
        ActivateDummies(false);
    }

    private void ActivateDummies(bool isActive)
    {
        foreach (var dummy in relicsDummies)
        {
            dummy.SetActive(isActive);
        }
    }

    private void SelectRandomConsumableObjects()
    {
        if (relicsPool.Length < relicsDummies.Length)
        {
            Debug.LogWarning("No hay suficientes consumibles en el pool.");
            return;
        }

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < relicsPool.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < relicsDummies.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            int selectedConsumableIndex = availableIndices[randomIndex];

            SetDummyConsumable(relicsDummies[i], relicsPool[selectedConsumableIndex]);

            availableIndices.RemoveAt(randomIndex);
        }
    }


    private void SetDummyConsumable(GameObject dummy, RelicObject relic)
    {
        RelicDisplay relicDisplay = dummy.GetComponent<RelicDisplay>();

        relicDisplay.relicObject = relic;
        relicDisplay.relicArt.sprite = relic.relicArt;

        Button dummyButton = dummy.GetComponent<Button>();
        dummyButton.onClick.RemoveAllListeners();
        dummyButton.onClick.AddListener(() => OnConsumableClick(relic));
    }

    public void OnConsumableClick(RelicObject relic)
    {

        int consumableIndex = relic.index;


        if (consumableIndex >= 0 && consumableIndex < relicsPrefabs.Length)
        {
            GameObject selectedPrefab = relicsPrefabs[consumableIndex];
            Instantiate(selectedPrefab, relicContainer);

            Debug.Log("Consumable instantiated: " + selectedPrefab.name);
        }
        else
        {
            Debug.LogWarning("Índice de consumible fuera de rango o inválido.");
        }
    }

}
