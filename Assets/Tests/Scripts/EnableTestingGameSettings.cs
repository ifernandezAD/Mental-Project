using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D.IK;

public class EnableTestingGameSettings : MonoBehaviour
{
    [SerializeField] GameObject[] allyArray;
    [SerializeField] GameObject[] consumablePrefabs;
    [SerializeField] GameObject[] relicsPrefabs;
    [SerializeField] GameObject[] symbolPrefabs;
    [SerializeField] ImageType[] symbolTypes;
    private Transform allyContainer;
    private Transform consumableContainer;
    private Transform relicContainer;

    void Awake()
    {
        allyContainer = GameLogic.instance.allyContainer;
        consumableContainer = GameLogic.instance.objectContainer;
        relicContainer = GameLogic.instance.relicContainer;
    }
    void Start()
    {

        RoundManager.instance.LoadTestingRoundManagerPreferences();
        TestingInstantiateAllies();
        TestingInstantiateConsumables();
        TestingInstantiateRelics();
        TestingInstantiateSymbols();
    }

    private void TestingInstantiateAllies()
    {
        int numberOfAllies = PlayerPrefs.GetInt("Testing_Allies", 0);


        numberOfAllies = Mathf.Clamp(numberOfAllies, 0, allyArray.Length);

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < allyArray.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < numberOfAllies; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int selectedAllyIndex = availableIndices[randomIndex];

            GameObject allyObject = Instantiate(allyArray[selectedAllyIndex], allyContainer);

            availableIndices.RemoveAt(randomIndex);
        }
    }

    private void TestingInstantiateConsumables()
    {
        int numberOfConsumables = PlayerPrefs.GetInt("Testing_Consumables", 0);

        numberOfConsumables = Mathf.Clamp(numberOfConsumables, 0, consumablePrefabs.Length);

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < consumablePrefabs.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < numberOfConsumables; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int selectedConsumableIndex = availableIndices[randomIndex];

            GameObject consumableObject = Instantiate(consumablePrefabs[selectedConsumableIndex], consumableContainer);

            availableIndices.RemoveAt(randomIndex);
        }
    }

    private void TestingInstantiateRelics()
    {
        int numberOfRelics = PlayerPrefs.GetInt("Testing_Relics", 0);


        numberOfRelics = Mathf.Clamp(numberOfRelics, 0, relicsPrefabs.Length);

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < relicsPrefabs.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < numberOfRelics; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int selectedRelicIndex = availableIndices[randomIndex];


            GameObject relicObject = Instantiate(relicsPrefabs[selectedRelicIndex], relicContainer);

            availableIndices.RemoveAt(randomIndex);
        }
    }

    private void TestingInstantiateSymbols()
{
    int numberOfSymbols = PlayerPrefs.GetInt("Testing_Symbols", 0);

    numberOfSymbols = Mathf.Clamp(numberOfSymbols, 0, consumablePrefabs.Length); 



    List<int> availableIndices = new List<int>();
    for (int i = 0; i < symbolPrefabs.Length; i++)
    {
        availableIndices.Add(i);
    }

    for (int i = 0; i < numberOfSymbols; i++)
    {
        int randomIndex = Random.Range(0, availableIndices.Count);
        int selectedSymbolIndex = availableIndices[randomIndex];

        
        Roller.instance.AddImagePrefab(symbolTypes[selectedSymbolIndex], symbolPrefabs[selectedSymbolIndex]);

        availableIndices.RemoveAt(randomIndex);
    }
}

}
