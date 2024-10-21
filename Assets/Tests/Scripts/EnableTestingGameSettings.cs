using UnityEngine;
using System.Collections.Generic;

public class EnableTestingGameSettings : MonoBehaviour
{
    [SerializeField] GameObject[] allyArray;
    private Transform allyContainer;

    void Awake()
    {
        allyContainer = GameLogic.instance.allyContainer;
    }
    void Start()
    {

        RoundManager.instance.LoadTestingRoundManagerPreferences();
        TestingInstantiateAllies();
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
}
