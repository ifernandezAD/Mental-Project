using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; 

public class MolePopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject molePopup;
    private TextMeshProUGUI popupText;
    [SerializeField] CardDisplay cardDisplay;

    void Awake()
    {
        molePopup = GameLogic.instance.molePopUp;
        popupText = molePopup.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        molePopup.SetActive(true);

        Debug.Log("Has entered");

        if (cardDisplay != null) 
        {
            Debug.Log("Card display found");
            popupText.text = cardDisplay.card.skillDescription; 
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        molePopup.SetActive(false);
    }

}
