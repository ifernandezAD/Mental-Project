using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; 

public class MolePopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject molePopup;
    private TextMeshProUGUI popupText;
    private CardDisplay cardDisplay;

    void Awake()
    {
        molePopup = GameLogic.instance.molePopUp;
        popupText = molePopup.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Entered");
        molePopup.SetActive(true);
        if (cardDisplay != null)
        {
            popupText.text = cardDisplay.card.skillDescription; 
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exited");
        molePopup.SetActive(false);
    }

}
