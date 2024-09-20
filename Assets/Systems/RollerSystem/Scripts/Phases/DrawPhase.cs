using System;
using UnityEngine;

public class DrawPhase : Phase
{
    [Header("Enemy Cards Variables")]
    [SerializeField] GameObject[] act1EnemyCardsArray;
    [SerializeField] GameObject[] act2EnemyCardsArray;
    [SerializeField] GameObject[] act3EnemyCardsArray;
    [SerializeField] GameObject testingBossPrefab;

    [Header("Game Events Variables")]
    [SerializeField] GameObject[] generalEventsArray;
    [SerializeField] Transform eventContainer;
    private int normalEventCount = 0;
    private bool lastDrawWasEvent = false;

    [Header("Ally Events Variables")]
    [SerializeField] GameObject allyEventPopup;
    private bool allyDrawnInCurrentAct = false;
    private int[] eventRounds = new int[] { 3, 6, 9 };
    private int chosenAllyRound;

    [Header("Relics")]
    public static Action onEventTriggered;

    protected override void InternalOnEnable()
    {
        base.InternalOnEnable();
        EventManager.onEventButtonPressed += OnEventButtonPressed;
    }

    protected override void BeginPhase()
    {
        if (RoundManager.instance.GetCurrentRound() == 1)
        {
            allyDrawnInCurrentAct = false;
            chosenAllyRound = UnityEngine.Random.Range(0, eventRounds.Length);
        }

        if (RoundManager.instance.IsBossRound())
        {
            ClearEnemyCardContainer();
            DrawBossCard();
        }
        else
        {
            DrawRandomCard();
        }
    }

    private void DrawRandomCard()
    {
        int roundNumber = RoundManager.instance.GetCurrentRound();
        int actNumber = RoundManager.instance.GetCurrentAct();

        if (roundNumber <= 2)
        {
            DrawEnemyCardByAct(actNumber);
            return;
        }

        if (Array.Exists(eventRounds, round => round == roundNumber))
        {
            if (actNumber == 3 && RoundManager.instance.allyCardContainer.childCount >= 2)
            {
                DrawEvent();
                return;
            }

            if (roundNumber == eventRounds[chosenAllyRound] && !allyDrawnInCurrentAct)
            {
                DrawAllyEvent();
                allyDrawnInCurrentAct = true;
            }
            else
            {
                DrawEvent();
            }
        }
        else
        {
            DrawEnemyCardByAct(actNumber);
        }
    }

    private void DrawEnemyCardByAct(int actNumber)
    {
        GameObject[] selectedArray = actNumber switch
        {
            1 => act1EnemyCardsArray,
            2 => act2EnemyCardsArray,
            3 => act3EnemyCardsArray,
            _ => null
        };

        int randomIndex = UnityEngine.Random.Range(0, selectedArray.Length);
        GameObject enemyCard = Instantiate(selectedArray[randomIndex], enemyCardContainer);
        StartCoroutine(StartNextPhaseWithDelayCorroutine());
    }

    private void ClearEnemyCardContainer()
    {
        foreach (Transform child in enemyCardContainer)
        {
            Destroy(child.gameObject);
        }
    }


    private void DrawBossCard()
    {
        GameObject card = Instantiate(testingBossPrefab, enemyCardContainer);
        StartCoroutine(StartNextPhaseWithDelayCorroutine());
    }

    private void DrawEvent()
    {
        DrawGeneralEvent();
    }

    private void DrawAllyEvent()
    {
        allyEventPopup.SetActive(true);
        onEventTriggered?.Invoke();
    }

    private void DrawGeneralEvent()
    {
        int randomIndex = UnityEngine.Random.Range(0, generalEventsArray.Length);
        GameObject eventCard = Instantiate(generalEventsArray[randomIndex], eventContainer);
        onEventTriggered?.Invoke();
    }

    private void OnEventButtonPressed()
    {
        ClearEventContainer();
        if (enemyCardContainer.childCount == 0)
        {
            StartNextRoundWithDelay();
            return;
        }
        StartCoroutine(StartNextPhaseWithDelayCorroutine());
    }

    private void ClearEventContainer()
    {
        foreach (Transform child in eventContainer)
        {
            Destroy(child.gameObject);
        }
    }

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        EventManager.onEventButtonPressed -= OnEventButtonPressed;
    }
}
