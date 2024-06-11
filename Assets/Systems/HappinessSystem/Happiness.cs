using System;
using UnityEngine;

public class Happiness : MonoBehaviour
{
    public static Action<int,int> onHappinessChange;

    [SerializeField] private int happinessRange =10;
    [SerializeField] private int currentHappiness =0;

    
    [Header("Testing")]
    [SerializeField] private bool testAddHappiness;
    [SerializeField] private bool testDecreaseHappiness;


    void OnValidate()
    {
        if(testAddHappiness)
        {
            AddHappiness(1);
            testAddHappiness=false;
        }

        if(testDecreaseHappiness)
        {
            DecreaseHappiness(-1);
            testDecreaseHappiness=false;
        }  
    }

    void AddHappiness(int happiness)
    {
        currentHappiness += happiness;
    
        if (currentHappiness >= happinessRange)
        {
            currentHappiness = happinessRange;
            
        }

        onHappinessChange?.Invoke(currentHappiness,happinessRange);
    }

    void DecreaseHappiness(int happiness)
    {
        currentHappiness += happiness;

        if (currentHappiness <= -happinessRange)
        {
            currentHappiness=-happinessRange;
        }

        onHappinessChange?.Invoke(currentHappiness,happinessRange);
    }
}
