using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoFeedbackDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CardDisplay cardDisplay;
    [SerializeField] private Consumable consumable;

    private bool isMouseOver = false;

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
                GameInfoPopup.instance.ShowPopUp(cardDisplay.card.skillDescription);
            }

             else if (consumable != null)
            {
                GameInfoPopup.instance.ShowPopUp(consumable.description);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        GameInfoPopup.instance.HidePopUp();
    }
}
