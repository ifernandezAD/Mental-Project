using UnityEngine;

public class StaminaNoAttackRelic : Relic
{
    [SerializeField] int staminaToIncrease = 1;
    private bool hasPlayedAttackSymbol = false;

    protected override void Effect()
    {
        PlayerPhase.onPlayerPhaseEnded += OnPlayerPhaseEnded;
        BubbleDetector.onSwordUsed += OnAttackSymbolPlayed;
    }

    public void OnAttackSymbolPlayed()
    {
        hasPlayedAttackSymbol = true;
    }

    private void OnPlayerPhaseEnded()
    {
        if (!hasPlayedAttackSymbol)
        {
            GainExtraStamina();
        }

        hasPlayedAttackSymbol = false; 
    }

    private void GainExtraStamina()
    {
        Skill skill = GameLogic.instance.mainCharacterCard.GetComponent<Skill>();
        skill.IncreaseStamina(staminaToIncrease);
    }

    private void OnDisable()
    {
        PlayerPhase.onPlayerPhaseEnded -= OnPlayerPhaseEnded;
        BubbleDetector.onSwordUsed -= OnAttackSymbolPlayed;
    }
}
