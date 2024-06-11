using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MentalHealthBar : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image mentalHealthBar;

    void OnEnable()
    {
        MentalHealth.onMentalHealthChange += UpdateHealthBar;
    }

    private void UpdateHealthBar(float fillValue)
    {
        mentalHealthBar.fillAmount = fillValue;
    }

     void OnDisable()
    {
        MentalHealth.onMentalHealthChange += UpdateHealthBar;
    }
}
