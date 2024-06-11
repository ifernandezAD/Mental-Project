using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MentalHealthBar : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image mentalHealthBar;
    [SerializeField] private TextMeshProUGUI mentalHealthText;

    void OnEnable()
    {
        MentalHealth.onMentalHealthChange += UpdateMentalHealthBar;
    }

    private void UpdateMentalHealthBar(float fillValue, int currentMentalHealth)
    {
        UpdateMentalHealthBarImage(fillValue);
        UpdateMentalHealthText(currentMentalHealth);
    }

    private void UpdateMentalHealthBarImage(float fillValue)
    {
        mentalHealthBar.fillAmount = fillValue;
    }

    private void UpdateMentalHealthText(int currentMentalHealth)
    {
        mentalHealthText.text =  currentMentalHealth + "/100";
    }

    void OnDisable()
    {
        MentalHealth.onMentalHealthChange -= UpdateMentalHealthBar;
    }
}
