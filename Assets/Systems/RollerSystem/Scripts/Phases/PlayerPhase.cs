using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerPhase : Phase
{
    [SerializeField] Button okButton;

    [Header("Bubbles")]
    [SerializeField] GameObject[] bubbles;
    [SerializeField] Transform bubbleContainer;

    protected override void InternalOnEnable()
    {
        base.InternalOnEnable();
        OKButton.onOKButtonPressed += StartNextPhaseWithDelay;
        Health.onBossDefeated += StartNextActWithDelay;
    }

    protected override void BeginPhase()
    {
        okButton.interactable = true;
        InstantiateBubbles();
    }

    private void InstantiateBubbles()
    {
        Roller roller = Roller.instance;
        roller.UpdateImageCount();

        int swordCount = roller.GetImageCount(ImageType.Sword);
        int heartCount = roller.GetImageCount(ImageType.Heart);
        int bookCount = roller.GetImageCount(ImageType.Book);
        int poisonCount = roller.GetImageCount(ImageType.Poison);
        int randomCount = roller.GetImageCount(ImageType.Random);

        for (int i = 0; i < swordCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[0], bubbleContainer);
        }

        for (int i = 0; i < heartCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[1], bubbleContainer);
        }

        for (int i = 0; i < bookCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[2], bubbleContainer);
        }

        for (int i = 0; i < poisonCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[3], bubbleContainer);
        }

        for (int i = 0; i < randomCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, bubbles.Length);
            GameObject bubble = Instantiate(bubbles[randomIndex], bubbleContainer);
        }
    }

    private void DestroyAllBubbles()
    {
        foreach (Transform bubble in bubbleContainer)
        {
              if (bubble.CompareTag("Poison"))
            {
                GameLogic.instance.ApplyDamageToRandomTarget(1);
            }

            Destroy(bubble.gameObject);
        }
    }

    void ResetResilience()
    {
        RoundManager.instance.characterCardContainer.GetChild(0).gameObject.GetComponent<Health>().ResetResilience();
    }

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        OKButton.onOKButtonPressed -= StartNextPhaseWithDelay;
        Health.onBossDefeated -= StartNextActWithDelay;

        okButton.interactable = false;
        DestroyAllBubbles();

        //ResetResilience(); //Testing, puede que se elimine esta funcionalidad
    }
}
