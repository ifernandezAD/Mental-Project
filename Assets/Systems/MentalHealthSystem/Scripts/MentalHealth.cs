using System;
using Unity.VisualScripting;
using UnityEngine;

public class MentalHealth : MonoBehaviour
{
    public static Action<int,int> onMentalHealthChange;

    [Header("Mental Health")]
    [SerializeField] private int maxMentalHealth = 100;
    [SerializeField] private int currentMentalHealth = 100;

    [Header("Testing")]
    [SerializeField] private bool testAddHealth;
    [SerializeField] private bool testDecreaseHealth;


    void OnValidate()
    {
        if(testAddHealth)
        {
            AddMentalHealth(5);
            testAddHealth=false;
        }

        if(testDecreaseHealth)
        {
            DecreaseMentalHealth(-5);
            testDecreaseHealth=false;
        }  
    }

    void AddMentalHealth(int mentalHealth)
    {
        currentMentalHealth += mentalHealth;
    
        if (currentMentalHealth >= maxMentalHealth)
        {
            currentMentalHealth = maxMentalHealth;
            
        }

        onMentalHealthChange?.Invoke(currentMentalHealth,maxMentalHealth);
    }

    void DecreaseMentalHealth(int mentalHealth)
    {
        currentMentalHealth += mentalHealth;

        if (currentMentalHealth <= 0)
        {
            currentMentalHealth=0;
            GameOver();
        }

        onMentalHealthChange?.Invoke(currentMentalHealth,maxMentalHealth);
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
    }
}
