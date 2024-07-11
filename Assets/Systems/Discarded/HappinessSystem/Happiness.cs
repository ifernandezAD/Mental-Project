using System;
using UnityEngine;
public class Happiness : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
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

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        //happinessRange = characterController.selectedCharacter.happinessRange;
        onHappinessChange?.Invoke(currentHappiness,happinessRange);
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
