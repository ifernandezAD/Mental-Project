using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerPhase : Phase
{
    [Header("UI Feedback")]
    [SerializeField] TextMeshProUGUI attacksLeftText;
    [SerializeField] TextMeshProUGUI resilienceLeftText;
    [SerializeField] TextMeshProUGUI skillsLeftText;
    private int attackClicks = 0;
    private int skillClicks = 0;

    protected override void BeginPhase()
    {
        CalculateButtonClicks();
        EnableEnemyCardsInteractivity();
        //EnableCharacterCardsInteractivity();

        //StartNextPhaseWithDelay();
    }



    void CalculateButtonClicks()
    {
        attackClicks = Roller.instance.GetImageCount(ImageType.Sword);

        skillClicks = Roller.instance.GetImageCount(ImageType.Book);
    }

    void ResetButtonClicks()
    {
        attackClicks = 0;

        skillClicks = 0;
    }
    private void DecreaseAttackCLicks()
    {
        attackClicks--;
    }

    private void EnableEnemyCardsInteractivity()
    {
        throw new NotImplementedException();
    }

    private void EnableCharacterCardsInteractivity()
    {
        throw new NotImplementedException();
    }

    private void DisableEnemyCardsInteractivity()
    {
        throw new NotImplementedException();
    }

    private void DisableCharacterCardsInteractivity()
    {
        throw new NotImplementedException();
    }

    void EndPhase()
    {
        ResetButtonClicks();
    }

}
