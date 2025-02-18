using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [Header("Common References")]
    [SerializeField] public Card card;
    [SerializeField] public Image cardArt;
    [SerializeField] public TextMeshProUGUI healthText; 
    [SerializeField] public TextMeshProUGUI staminaText;

    [Header("Enemy References")]
    [SerializeField] public TextMeshProUGUI attackText;

    void Start()
    {
        cardArt.sprite = card.art;
        healthText.text = card.maxHealth.ToString();
        staminaText.text= 0.ToString();

        if (card.cardType == Card.CardType.Enemy)
        {
            attackText.text = card.attack.ToString();
            staminaText.text = "";
        }
    }

}
