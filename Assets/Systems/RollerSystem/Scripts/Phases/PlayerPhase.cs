using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerPhase : Phase
{
    [SerializeField] Button okButton;

    [Header("Bubbles")]
    [SerializeField] GameObject attackBubble;
    [SerializeField] GameObject resilienceBubble;
    [SerializeField] GameObject staminaBubble;


    [Header("Bubbles Containers")] 
    [SerializeField] Transform attackBubbleContainer;
    [SerializeField] Transform resilienceBubbleContainer;
    [SerializeField] Transform staminaBubbleContainer;

    private List<GameObject> attackBubbles = new List<GameObject>();
    private List<GameObject> resilienceBubbles = new List<GameObject>();
    private List<GameObject> staminaBubbles = new List<GameObject>();

    protected override void InternalOnEnable()
    {
        base.InternalOnEnable();
        OKButton.onOKButtonPressed += StartNextPhaseWithDelay;
        Health.onBossDefeated += StartNextActWithDelay; 
    }

    protected override void BeginPhase()
    {
        okButton.interactable=true;

        InstantiateBubbles();
    }

    private void InstantiateBubbles()
    {
        int swordCount = Roller.instance.GetImageCount(ImageType.Sword);
        int heartCount = Roller.instance.GetImageCount(ImageType.Heart);
        int bookCount = Roller.instance.GetImageCount(ImageType.Book);

        for (int i = 0; i < swordCount; i++)
        {
            GameObject bubble = Instantiate(attackBubble, attackBubbleContainer);
            attackBubbles.Add(bubble);
        }

        for (int i = 0; i < heartCount; i++)
        {
            GameObject bubble = Instantiate(resilienceBubble, resilienceBubbleContainer);
            resilienceBubbles.Add(bubble);
        }

        for (int i = 0; i < bookCount; i++)
        {
            GameObject bubble = Instantiate(staminaBubble, staminaBubbleContainer);
            staminaBubbles.Add(bubble);
        }
    }

    private void DestroyAllBubbles()
    {
        foreach (GameObject bubble in attackBubbles)
        {
            Destroy(bubble);
        }
        attackBubbles.Clear();

        foreach (GameObject bubble in resilienceBubbles)
        {
            Destroy(bubble);
        }
        resilienceBubbles.Clear();

        foreach (GameObject bubble in staminaBubbles)
        {
            Destroy(bubble);
        }
        staminaBubbles.Clear();
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

        okButton.interactable=false;
        DestroyAllBubbles();

        //ResetResilience(); //Testing, puede que se elimine esta funcionalidad
    }
}
