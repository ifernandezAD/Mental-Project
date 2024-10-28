using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        button.interactable = false;
        onOKButtonPressed?.Invoke();

        if(bossDefeated)
        {
            Debug.Log("Boss Defeated");
            PlayerPhase.instance.StartNextActWithDelay();
            PlayerPhase.instance.ResetBossPartsCount();
            bossDefeated=false;
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
        bossDefeated =true;
    }
}
