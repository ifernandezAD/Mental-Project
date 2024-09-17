using UnityEngine;
using TMPro;

public class GameInfoPopup : MonoBehaviour
{
    public static GameInfoPopup instance { get; private set; }

    [SerializeField] private GameObject molePopup;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;


    private void Awake()
    {
        instance = this;

        molePopup.SetActive(false); 
    }

    public void ShowPopUp(string name, string description)
    {
        nameText.text = name;
        descriptionText.text = description;
        molePopup.SetActive(true);
    }

    public void HidePopUp()
    {
        molePopup.SetActive(false);
    }
}
