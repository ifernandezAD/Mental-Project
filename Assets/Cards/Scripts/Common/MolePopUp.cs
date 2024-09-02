using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MolePopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject molePopup;
    private TextMeshProUGUI popupText;
    [SerializeField] CardDisplay cardDisplay;

    private bool isMouseOver = false;

    void Awake()
    {
        molePopup = GameLogic.instance.molePopUp;
        popupText = molePopup.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonUp(1))
        {
            molePopup.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;

        if (Input.GetMouseButton(1)) 
        {
            molePopup.SetActive(true);

            Debug.Log("Has entered with right mouse button held down");

            if (cardDisplay != null) 
            {
                Debug.Log("Card display found");
                popupText.text = cardDisplay.card.skillDescription;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        molePopup.SetActive(false);
    }
}
