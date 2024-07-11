using System;
using UnityEngine;
using UnityEngine.UI;

public class RollButton : MonoBehaviour
{
    private Button button;
    public static Action onRoll;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(RollOnce);
    }
    void OnEnable()
    {
        OKButton.onOKButtonPressed +=DisableButton;
        Energy.onResetEnergy += EnableButton;
    }

    private void RollOnce() 
    {
        Roller.instance.ActivateRandomImageInSlots();
        onRoll?.Invoke();
    }
   
    void EnableButton() { button.interactable = true; }
    void DisableButton(){button.interactable = false;}
   
    void OnDisable()
    {
        OKButton.onOKButtonPressed -=DisableButton;
        Energy.onResetEnergy -= EnableButton;
    }

}
