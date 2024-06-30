using UnityEngine;
using TMPro;

public class MentalHealthBar : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image mentalHealthBar;
    [SerializeField] private TextMeshProUGUI mentalHealthText;

    void OnEnable()
    {
        MentalHealth.onMentalHealthChange += UpdateMentalHealthBar;
    }

    private void UpdateMentalHealthBar(int currentMentalHealth, int maxMentalHealth)
    {
        CalculateMentalHealthBarFillAmount(currentMentalHealth, maxMentalHealth);
        UpdateMentalHealthText(currentMentalHealth, maxMentalHealth);
    }

    void CalculateMentalHealthBarFillAmount(float currentMentalHealth, int maxMentalHealth)
    {
        float fillValue = (float)currentMentalHealth / maxMentalHealth;
        UpdateMentalHealthBarImage(fillValue);
    }

    private void UpdateMentalHealthBarImage(float fillValue)
    {
        mentalHealthBar.fillAmount = fillValue;
    }

    private void UpdateMentalHealthText(int currentMentalHealth, int maxMentalHealth)
    {
        mentalHealthText.text = currentMentalHealth + " / " + maxMentalHealth;
    }

    void OnDisable()
    {
        MentalHealth.onMentalHealthChange -= UpdateMentalHealthBar;
    }
}
