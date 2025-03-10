using UnityEngine;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    public static LightController instance { get; private set; }

    [Header("Light Settings")]
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D globalLight;
    [SerializeField] private float[] lightIntensityLevels = { 1.0f, 0.75f, 0.5f, 0.2f }; 
    [SerializeField] private float flashDuration = 0.5f; 

    private int currentLightLevel = 1; 
    private const int MAX_LIGHT_LEVEL = 3;
    private const int MIN_LIGHT_LEVEL = 0;

    private float currentIntensity;
    private void Awake()
    {
        instance = this;
        UpdateLightIntensity();
    }

    public void IncreaseLight()
    {
        if (currentLightLevel < MAX_LIGHT_LEVEL)
        {
            currentLightLevel++;
            UpdateLightIntensity();
        }
        else
        {
            BlindingFlash();
        }
    }

    public void DecreaseLight()
    {
        if (currentLightLevel > MIN_LIGHT_LEVEL)
        {
            currentLightLevel--;
            UpdateLightIntensity();
        }
        else
        {
            TotalDarkness();
        }
    }

    private void UpdateLightIntensity()
    {
        float targetIntensity = lightIntensityLevels[currentLightLevel];
        DOTween.To(() => currentIntensity, x =>
        {
            currentIntensity = x;
            globalLight.intensity = currentIntensity;
        }, targetIntensity, 0.5f);
    }

    private void BlindingFlash()
    {
        DOTween.To(() => currentIntensity, x =>
        {
            currentIntensity = x;
            globalLight.intensity = currentIntensity;
        }, 1.5f, flashDuration).OnComplete(() =>
        {
            UpdateLightIntensity(); // Vuelve al nivel máximo después del flash
            // Inflige 1 de daño al jugador aquí
        });
    }

    private void TotalDarkness()
    {
        Debug.Log("Oscuridad total: El jefe se potencia");
    }


    #region Testing
    [Header("Test Controls")]
    [SerializeField] private bool testIncreaseLight;
    [SerializeField] private bool testDecreaseLight;

    private void OnValidate()
    {
        if (testIncreaseLight)
        {
            testIncreaseLight = false;
            IncreaseLight();
        }

        if (testDecreaseLight)
        {
            testDecreaseLight = false;
            DecreaseLight();
        }
    }

    #endregion
}
