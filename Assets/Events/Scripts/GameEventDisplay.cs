using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEventDisplay : MonoBehaviour
{
    [Header("Common References")]
    [SerializeField] public GameEvent gameEvent;
    [SerializeField] Image eventArt;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI firstButtonText;
    [SerializeField] TextMeshProUGUI secondButtonText;
    [SerializeField] TextMeshProUGUI thirdButtonText;


    void Start()
    {
        eventArt.sprite = gameEvent.art;
        titleText.text = gameEvent.eventName;    
        firstButtonText.text = gameEvent.firstButtonText;
        secondButtonText.text = gameEvent.secondButtonText;
        thirdButtonText.text = gameEvent.thirdButtonText;
    }

}
