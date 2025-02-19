using System.Collections.Generic;
using UnityEngine;

public class SymbolEvent : GenericEvent
{
    [SerializeField] GameObject[] symbolsPrefabs;
    [SerializeField] GameObject badSymbolPrefab;
    [SerializeField] Transform symbolContainer;

    public override void Initialize(bool isFlashback)
    {
        base.Initialize(isFlashback);
        SelectRandomSymbols();
    }

    private void OnDisable()
    {
        ClearSymbols();
    }

    private void SelectRandomSymbols()
    {
        if (IsFlashback && !IsGoodFlashback) 
        {
            InstantiateSymbol(badSymbolPrefab);
            return;
        }

        int symbolsToSelect = (IsFlashback && IsGoodFlashback) ? 3 : 1;

        if (symbolsPrefabs.Length < symbolsToSelect)
        {
            Debug.LogWarning("No hay suficientes símbolos en el pool.");
            return;
        }

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < symbolsPrefabs.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < symbolsToSelect; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            int selectedSymbolIndex = availableIndices[randomIndex];

            InstantiateSymbol(symbolsPrefabs[selectedSymbolIndex]);

            availableIndices.RemoveAt(randomIndex);
        }
    }

    private void InstantiateSymbol(GameObject symbolPrefab)
    {
        Instantiate(symbolPrefab, symbolContainer);
    }

    private void ClearSymbols()
    {
        foreach (Transform symbol in symbolContainer)
        {
            Destroy(symbol.gameObject);
        }
    }
}
