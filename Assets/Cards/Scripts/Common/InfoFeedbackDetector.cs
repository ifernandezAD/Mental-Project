using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoFeedbackDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CardDisplay cardDisplay;
    [SerializeField] private ConsumableDisplay consumableDisplay;
    [SerializeField] private Relic relic;

    private bool isMouseOver = false;

    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
        consumableDisplay = GetComponent<ConsumableDisplay>();
        relic = GetComponent<Relic>();
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonUp(1))
        {
            GameInfoPopup.instance.HidePopUp();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;

        if (Input.GetMouseButton(1))
        {
            if (cardDisplay != null)
            {
                GameInfoPopup.instance.ShowPopUp(cardDisplay.card.skillName ,cardDisplay.card.skillDescription);
            }

            else if (consumableDisplay != null)
            {
                GameInfoPopup.instance.ShowPopUp(consumableDisplay.consumableObject.consumableName,consumableDisplay.consumableObject.consumableDescription);
            }
            else if (relic != null)
            {
                GameInfoPopup.instance.ShowPopUp(relic.relicName,relic.relicDescription);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        GameInfoPopup.instance.HidePopUp();
    }
}
