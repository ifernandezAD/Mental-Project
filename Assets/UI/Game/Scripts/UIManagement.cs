using UnityEngine;

public class UIManagement : MonoBehaviour
{
    public static UIManagement instance { get; private set; }

    [Header("Act Image Container")]
    [SerializeField] private Transform actImageContainer;

    [Header("Lights Container")]
    [SerializeField] private Transform lightsContainer;
    private int currentLightIndex = 0;

    [Header("Curtain")]
    [SerializeField] GameObject curtainContainer;
    [SerializeField] Animator curtainAnimator;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetActiveActImage(0);
        curtainContainer.SetActive(true);
    }

    #region Lights
    public void TurnOnNextLight()
    {
        if (currentLightIndex < lightsContainer.childCount)
        {
            Transform light = lightsContainer.GetChild(currentLightIndex);
            light.GetChild(0).gameObject.SetActive(false);
            light.GetChild(1).gameObject.SetActive(true);

            currentLightIndex++;
        }
    }

    public void ResetAllLights()
    {
        currentLightIndex = 0;
        foreach (Transform light in lightsContainer)
        {
            light.GetChild(0).gameObject.SetActive(true);
            light.GetChild(1).gameObject.SetActive(false);
        }
    }

    #endregion
    public void SetActiveActImage(int actNumber)
    {
        for (int i = 0; i < actImageContainer.childCount; i++)
        {
            actImageContainer.GetChild(i).gameObject.SetActive(i == actNumber);
        }
    }

    public void CloseCurtain()
    {
        curtainAnimator.SetTrigger("Close");
    }

    public void OpenCurtain()
    {
        curtainAnimator.SetTrigger("Open");
    }
}
