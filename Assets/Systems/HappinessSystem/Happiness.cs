using System;
using UnityEngine;

public class Happiness : MonoBehaviour
{
    public static Action<int,int> onHappinessChange;

    [SerializeField] private int happinessRange =10;
    [SerializeField] private int currentHappinessRange =0;

    
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
        currentHappinessRange += happiness;
    
        if (currentHappinessRange >= happinessRange)
        {
            currentHappinessRange = happinessRange;
            
        }

        //onMentalHealthChange?.Invoke(currentMentalHealth,maxMentalHealth);
    }

    void DecreaseHappiness(int happiness)
    {
        currentHappinessRange += happiness;

        if (currentHappinessRange <= -happinessRange)
        {
            currentHappinessRange=-happinessRange;
        }

        //onMentalHealthChange?.Invoke(currentMentalHealth,maxMentalHealth);
    }
}
