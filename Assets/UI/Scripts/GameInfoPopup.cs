using UnityEngine;
using TMPro;

public class GameInfoPopup : MonoBehaviour
{
    public static GameInfoPopup instance { get; private set; }

    [SerializeField] private GameObject molePopup;
    [SerializeField] private TextMeshProUGUI popupText;

    private void Awake()
    {
        instance = this;

        molePopup.SetActive(false); 
    }

    public void ShowPopUp(string text)
    {
        popupText.text = text;
        molePopup.SetActive(true);
    }

    public void HidePopUp()
    {
        molePopup.SetActive(false);
    }
}
