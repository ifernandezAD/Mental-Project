using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterCardDisplay : MonoBehaviour
{
 [SerializeField] public CharacterCard characterCard;
 [SerializeField] Image cardArt;
 [SerializeField] TextMeshProUGUI lifeText;
 [SerializeField] TextMeshProUGUI skillText;

 void Start()
 {
    cardArt.sprite = characterCard.art;

    lifeText.text = "Life: " + characterCard.maxMentalHealth;
    skillText.text = characterCard.skill;
 }
}
