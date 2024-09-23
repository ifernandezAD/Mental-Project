using UnityEngine;

public class GainStaminaForAttacksUsedRelic : Relic
{
    [SerializeField] int staminaToIncrease = 1;
    private int attackSymbolCount = 0;
    private bool hasGainedStamina = false;

    protected override void Effect()
    {
        PlayerPhase.onPlayerPhaseEnded += ResetAttackCount;  
        BubbleDetector.onSwordUsed += OnAttackSymbolPlayed;
    }

    public void OnAttackSymbolPlayed()
    {
        attackSymbolCount++;
        if (attackSymbolCount >= 3 && !hasGainedStamina)
        {
            GainStamina();
        }
    }

    private void GainStamina()
    {
        Skill skill = GameLogic.instance.mainCharacterCard.GetComponent<Skill>();

        skill.IncreaseStamina(staminaToIncrease); 
        hasGainedStamina = true;     
    }

    private void ResetAttackCount()
    {
        attackSymbolCount = 0;
        hasGainedStamina = false; 
    }

    private void OnDisable()
    {
        PlayerPhase.onPlayerPhaseEnded -= ResetAttackCount;  
        BubbleDetector.onSwordUsed -= OnAttackSymbolPlayed;
    }
}
