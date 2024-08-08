using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    [Header("Ally Related Variables")]
    [SerializeField] Transform allyContainer; 
    [SerializeField] GameObject[] allyArray;
    private List<GameObject> remainingAllies;


    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        EventManager.onAllyObtained += AddRandomAlly;
    }

    private void AddRandomAlly()
    {
        int randomIndex = UnityEngine.Random.Range(0, remainingAllies.Count);
        GameObject selectedAlly = remainingAllies[randomIndex];

        GameObject newAlly = Instantiate(selectedAlly, allyContainer);

        remainingAllies.RemoveAt(randomIndex);
    }

    void OnDisable()
    {
        EventManager.onAllyObtained -= AddRandomAlly;
    }
}
