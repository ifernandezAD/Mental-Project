using UnityEngine;
using TMPro;
public class SharkCombat : CombatBehaviour
{
    [SerializeField] public TextMeshProUGUI attackText;
    private int temporaryAttack;

      void Start()
    {
        temporaryAttack = cardDisplay.card.attack;
    }
    public override void Attack()
    {    
        StatsManager.instance.ApplyDamageToRandomTarget(temporaryAttack);
        temporaryAttack++;
        attackText.text=temporaryAttack.ToString();
    }
}
