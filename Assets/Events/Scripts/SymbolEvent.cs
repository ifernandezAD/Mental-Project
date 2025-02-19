using System.Collections.Generic;
using UnityEngine;

public class SymbolEvent : GenericEvent
{
    [SerializeField] GameObject[] symbolsPrefabs;
    [SerializeField] GameObject badSymbolPrefab;
    [SerializeField] Transform symbolContainer;

    private void OnEnable()
    {
        SelectRandomSymbols();
    }

    private void OnDisable()
    {
        ClearSymbols();
    }

    private void SelectRandomSymbols()
    {
        if (symbolsPrefabs.Length < 3)
        {
            Debug.LogWarning("No hay suficientes símbolos en el pool.");
            return;
        }

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < symbolsPrefabs.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            int selectedSymbolIndex = availableIndices[randomIndex];

            InstantiateSymbol(symbolsPrefabs[selectedSymbolIndex]);

            availableIndices.RemoveAt(randomIndex);
        }
    }

    private void InstantiateSymbol(GameObject symbolPrefab)
    {
        GameObject symbolInstance = Instantiate(symbolPrefab, symbolContainer);
    }

    private void ClearSymbols()
    {
        foreach (Transform symbol in symbolContainer)
        {
            Destroy(symbol.gameObject);
        }
    }
}
