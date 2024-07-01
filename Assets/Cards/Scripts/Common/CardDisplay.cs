using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [Header("Common References")]
    [SerializeField] public Card card;
    [SerializeField] Image cardArt;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI skillText;
    [SerializeField] public TextMeshProUGUI staminaText;

    [Header("Enemy References")]
    [SerializeField] TextMeshProUGUI attackText;


    void Start()
    {
        cardArt.sprite = card.art;
        healthText.text = "Life: " + card.maxHealth;
        staminaText.text= "Sta: " + card.staminaCost;
        skillText.text = card.skillDescription;

        if (card.cardType == Card.CardType.Enemy)
        {
            attackText.text = card.attack.ToString();
            staminaText.text = "";
        }
    }

}
