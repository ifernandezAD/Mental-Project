using System;
using Unity.VisualScripting;
using UnityEngine;

public class MentalHealth : MonoBehaviour
{
    public static Action<float,int> onMentalHealthChange;

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
    
        if (currentMentalHealth >= 100)
        {
            currentMentalHealth = 100;
            
        }

         CalculateMentalHealthBarFillAmount();
    }

    void DecreaseMentalHealth(int mentalHealth)
    {
        currentMentalHealth += mentalHealth;
        CalculateMentalHealthBarFillAmount();

        if (currentMentalHealth <= 0)
        {
            GameOver();
        }
    }

    void CalculateMentalHealthBarFillAmount()
    {
        float fillValue = (float)currentMentalHealth / maxMentalHealth;

        onMentalHealthChange(fillValue,currentMentalHealth);
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
    }
}
