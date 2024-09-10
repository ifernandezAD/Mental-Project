using UnityEngine;
using TMPro;
public class SharkAttack : AttackBehaviour
{
    [SerializeField] public TextMeshProUGUI attackText;
    private int temporaryAttack;

      void Start()
    {
        temporaryAttack = cardDisplay.card.attack;
    }
    public override void Attack()
    {    
        AttackManager.instance.ApplyDamageToRandomTarget(temporaryAttack);
        temporaryAttack++;
        attackText.text=temporaryAttack.ToString();
    }
}
