using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OKButton : MonoBehaviour
{
    private Button button;
    private bool isOutOfEnergy;
    private bool bossDefeated;
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
    }
    void Start()
    {
        button.interactable = false;
    }

    private void DisableAndLaunchEvent()
    {
        if (bossDefeated)
        {
            UIManagement.instance.CloseCurtain();
            DOVirtual.DelayedCall(1, () => { PlayerPhase.instance.StartNextActWithDelay(); });

            PlayerPhase.instance.ResetBossPartsCount();
            bossDefeated = false;
            button.interactable = false;
        }
        else
        {
            button.interactable = false;
            onOKButtonPressed?.Invoke();
        }
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
    }

    public void ActivateBossDefeated()
    {
        bossDefeated = true;
    }
}
