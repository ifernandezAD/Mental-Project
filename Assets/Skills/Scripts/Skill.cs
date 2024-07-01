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

    private void Awake() { InternalAwake(); }
    protected virtual void InternalAwake()
    {
        maxStamina = GetComponent<CardDisplay>().card.staminaCost;
        staminaText = GetComponent<CardDisplay>().staminaText;
    }

    private void Start(){InternalStart();}
    protected virtual void InternalStart()
    {
        currentStamina = maxStamina;
    }

    public void DecreaseStamina(int stamina)
    {
        currentStamina -= stamina;
        staminaText.text = "Sta: " + currentStamina;

        CheckCurrentStamina();
    }

    private void CheckCurrentStamina()
    {
        if(currentStamina > 0)
        {
            return;
        }
        else
        {
            TriggerSkill();
        }
    }

    public void ResetStamina()
    {
        currentStamina = maxStamina;
        staminaText.text = "Sta: " + currentStamina;
    }

    public virtual void TriggerSkill()
    {
        ResetStamina();
    }
}
