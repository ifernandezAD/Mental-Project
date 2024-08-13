using TMPro;
using UnityEngine;

public class MolePopUp : MonoBehaviour
{
    private GameObject molePopup;
    private TextMeshProUGUI popupText;
    private CardDisplay cardDisplay;

    void Awake()
    {
        molePopup = GameLogic.instance.molePopUp;
        popupText = molePopup.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        cardDisplay = GetComponent<CardDisplay>();
    }
    void OnMouseEnter()
    {
        molePopup.SetActive(true);
        popupText.text = cardDisplay.card.skillDescription;
    }

    void OnMouseExit()
    {
        molePopup.SetActive(false);
    }
}
