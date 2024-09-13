using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public abstract class Skill : MonoBehaviour
{
    private int maxStamina = 0;
    private int currentStamina = 0; 
    private TextMeshProUGUI staminaText;
    protected Health health;

    private void Awake() { InternalAwake(); }
    protected virtual void InternalAwake()
    {
        maxStamina = GetComponent<CardDisplay>().card.staminaCost;
        staminaText = GetComponent<CardDisplay>().staminaText;
        health = GetComponent<Health>();
    }

    private void Start() { InternalStart(); }
    protected virtual void InternalStart()
    {
        if (GetComponent<CardDisplay>().card.cardType == Card.CardType.Ally)
        {
            AddSlot();
        }

        currentStamina = 0;
        staminaText.text = currentStamina.ToString();
    }

    void AddSlot()
    {
        Slots.instance.AddSlot();
    }

    public void IncreaseStamina(int stamina)
    {
        currentStamina += stamina;

        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            TriggerSkill();  
        }

        staminaText.text = currentStamina.ToString();
    }

       public void DecreaseStamina(int stamina)
    {
        currentStamina -= stamina;

        if (currentStamina < 0)
        {
            currentStamina = 0;
        }

        staminaText.text = currentStamina.ToString();
    }

    public void ResetStamina()
    {
        currentStamina = 0; 
        staminaText.text = currentStamina.ToString();
    }

    public virtual void TriggerSkill()
    {
        ResetStamina();  
    }
}
