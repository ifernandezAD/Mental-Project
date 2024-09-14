using UnityEngine;
using UnityEngine.UI;

public class ActivateOnPress : MonoBehaviour
{
    private Button button;
    private Consumable consumable;
    private const int playerPhaseIndex = 2;

    void Awake()
    {
        button = GetComponent<Button>();
        consumable = GetComponent<Consumable>();
        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (RoundManager.instance.currentPhaseIndex == playerPhaseIndex)
        {
            button.interactable = true; 
        }
        else
        {
            button.interactable = false; 
        }
    }

    void OnButtonClick()
    {
        if (button.interactable) 
        {
            consumable.Use();
        }
    }
}
