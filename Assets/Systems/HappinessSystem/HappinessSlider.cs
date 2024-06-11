using UnityEngine;
using UnityEngine.UI;

public class HappinessSlider : MonoBehaviour
{
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private Image happinessFill;

        void OnEnable()
    {
        Happiness.onHappinessChange += UpdateSlider;
    }

  void UpdateSlider(int currentHappiness, int happinessRange)
    {
        happinessSlider.minValue = -happinessRange;
        happinessSlider.maxValue = happinessRange;
        happinessSlider.value = currentHappiness;
        
        UpdateSliderColor(currentHappiness);
    }

        private void UpdateSliderColor(int currentHappiness)
    {
        if (currentHappiness < 0)
        {
            happinessFill.color = Color.red;
        }
        else if (currentHappiness > 0)
        {
            happinessFill.color = Color.green;
        }
        else
        {
            happinessFill.color = Color.yellow; 
        }
    }

      void OnDisable()
    {
        Happiness.onHappinessChange -= UpdateSlider;
    }
}
