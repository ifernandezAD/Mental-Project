using System;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    [Header("Ally Related Variables")]
    [SerializeField] Transform allyContainer; 
    [SerializeField] GameObject[] allyArray;


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
        int randomIndex = UnityEngine.Random.Range(0, allyArray.Length);
        GameObject newAlly = Instantiate(allyArray[randomIndex], allyContainer);
    }

    void OnDisable()
    {
        EventManager.onAllyObtained -= AddRandomAlly;
    }
}
