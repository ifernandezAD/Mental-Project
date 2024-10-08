using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OKButton : MonoBehaviour
{
    private Button button;
    private bool isOutOfEnergy;
    public static Action onOKButtonPressed;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(DisableAndLaunchEvent);
    }

    void OnEnable()
    {
        RollButton.onRoll += EnableButton;
        Energy.onOutOfEnergy += DisableAndLaunchEventWithoutEnergy;
        Health.onBossDefeated += DisableButton;
    }
    void Start()
    {
        button.interactable = false;
    }

    private void DisableAndLaunchEvent()
    {
        button.interactable = false;
        onOKButtonPressed?.Invoke();
    }

    private void DisableAndLaunchEventWithoutEnergy()
    {
        button.interactable = false;
        isOutOfEnergy = true;
        onOKButtonPressed?.Invoke();
    }

    void EnableButton()
    {
        if (!isOutOfEnergy)
        {
            button.interactable = true;
        }
        else
        {
            isOutOfEnergy = false;
        }
    }

    void DisableButton()
    {
        button.interactable = false;
    }

    void OnDisable()
    {
        RollButton.onRoll -= EnableButton;
        Energy.onOutOfEnergy -= DisableAndLaunchEventWithoutEnergy;
        Health.onBossDefeated -= DisableButton;
    }
}
