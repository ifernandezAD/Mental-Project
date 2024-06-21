using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class EnemyCardDisplay : MonoBehaviour
{
 [SerializeField] EnemyCard enemyCard;

 [SerializeField] Image cardArt;
 [SerializeField] TextMeshProUGUI lifeText;
 [SerializeField] TextMeshProUGUI attackText;
 [SerializeField] TextMeshProUGUI skillText;

 void Start()
 {
    cardArt.sprite = enemyCard.art;

    lifeText.text = "Life: " + enemyCard.health;
    attackText.text = enemyCard.attack.ToString();
    skillText.text = enemyCard.skill;
 }

}
