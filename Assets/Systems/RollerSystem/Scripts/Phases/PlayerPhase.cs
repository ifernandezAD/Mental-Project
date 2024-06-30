using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerPhase : Phase
{
    [Header("Bubbles")]
    [SerializeField] GameObject attackBubble;
    [SerializeField] GameObject resilienceBubble;
    [SerializeField] GameObject staminaBubble;

    
    [Header("Bubbles Containers")] //It will only be one when bubbles appears in random positions
    [SerializeField] Transform attackBubbleContainer;
    [SerializeField] Transform resilienceBubbleContainer;
    [SerializeField] Transform staminaBubbleContainer;
    
    protected override void BeginPhase()
    {
        InstantiateBubbles();

        //StartNextPhaseWithDelay();
    }

    private void InstantiateBubbles()
    {
        int swordCount = Roller.instance.GetImageCount(ImageType.Sword);
        int heartCount = Roller.instance.GetImageCount(ImageType.Heart);
        int bookCount = Roller.instance.GetImageCount(ImageType.Book);

        for (int i = 0; i < swordCount; i++)
        {
            Instantiate(attackBubble, attackBubbleContainer);
        }

        for (int i = 0; i < heartCount; i++)
        {
            Instantiate(resilienceBubble, resilienceBubbleContainer);
        }

        for (int i = 0; i < bookCount; i++)
        {
            Instantiate(staminaBubble, staminaBubbleContainer);
        }
    }
}
