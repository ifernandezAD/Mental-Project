using UnityEngine;
using UnityEngine.UI;


public class EnemyCardDisplay : MonoBehaviour
{
 [SerializeField] EnemyCard enemyCard;

 [SerializeField] Image cardArt;

 void Start()
 {
    cardArt.sprite = enemyCard.art;
 }

}
