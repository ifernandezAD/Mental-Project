using UnityEngine;
using UnityEngine.UI;

public class ActivateOnPress : MonoBehaviour
{
    private Button button;
    private Consumable consumable;

    void Awake()
    {
        button = GetComponent<Button>();
        consumable = GetComponent<Consumable>();
        button.onClick.AddListener(OnButtonClick);
    }

    void Start()
    {
        DisableButton();
    }
    void OnEnable()
    {
        PlayerPhase.onPlayerPhaseBegin += EnableButton;
        PlayerPhase.onPlayerPhaseEnded += DisableButton;
    }

    void EnableButton()
    {
        button.interactable = true;
    }

    void DisableButton()
    {
        button.interactable = false;
    }

    void OnButtonClick()
    {
        if (button.interactable) 
        {
            consumable.Use();
            Destroy(this.gameObject);
        }
    }

    
    void OnDisable()
    {
        PlayerPhase.onPlayerPhaseBegin -= EnableButton;
        PlayerPhase.onPlayerPhaseEnded -= DisableButton;
    }

}
