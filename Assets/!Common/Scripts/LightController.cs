using DG.Tweening;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public static LightController instance { get; private set; }

    [Header("Light Settings")]
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D globalLight;
    [SerializeField] private float[] lightIntensityLevels;

    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private float flashIntensity = 15f;

    private int currentLightLevel;
    private const int MAX_LIGHT_LEVEL = 3;
    private const int MIN_LIGHT_LEVEL = 0;

    private float currentIntensity;

    [Header("Initial Intensity")]
    [SerializeField] private int initialLightLevel = 1; 
    [SerializeField] private float lightDecreasingDuration = 0.5f;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        SetFirstBossLightIntensity();
    }


    public void SetFirstBossLightIntensity()
    {
        currentIntensity = globalLight.intensity;
        //float targetIntensity = lightIntensityLevels[MIN_LIGHT_LEVEL + 1];
        float targetIntensity = 0.1f;
        DOTween.Kill(globalLight);
        DOTween.To(() => currentIntensity, x =>
        {
            currentIntensity = x;
            globalLight.intensity = currentIntensity;
        }, targetIntensity, lightDecreasingDuration);
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
        float targetIntensity = lightIntensityLevels[MAX_LIGHT_LEVEL - currentLightLevel];

        DOTween.Kill(globalLight);  
        DOTween.To(() => currentIntensity, x =>
        {
            currentIntensity = x;
            globalLight.intensity = currentIntensity;
            Debug.Log($"Luz actualizada a: {currentIntensity} (Nivel: {currentLightLevel})");
        }, targetIntensity, 0.5f);
    }

    private void BlindingFlash()
    {
        DOTween.Kill(globalLight);
        DOTween.To(() => currentIntensity, x =>
        {
            currentIntensity = x;
            globalLight.intensity = currentIntensity;
        }, flashIntensity, flashDuration).OnComplete(() =>
        {
            UpdateLightIntensity(); 
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
