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
    [SerializeField, Range(0, 100)] private int eventDrawProbability = 20;

    [Header("Ally Events Variables")]
    [SerializeField] GameObject[] allyEventsArray;

    [SerializeField] private int minRoundForAllyEvent = 3;
    [SerializeField] private int maxRoundForAllyEvent = 8;
    [SerializeField, Range(0, 100)] private int allyEventProbability = 50;
    private bool allyDrawnInCurrentAct = false;

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

        if (ShouldDrawAllyEvent(roundNumber, actNumber))
        {
            DrawAllyEvent();
        }
        else
        {
            int randomValue = UnityEngine.Random.Range(0, 100);

            if (randomValue < eventDrawProbability)
            {
                DrawEvent();
            }
            else
            {
                DrawEnemyCardByAct(actNumber);
            }
        }
    }

    private void DrawEnemyCardByAct(int actNumber)
    {
        GameObject[] selectedArray;

        if (actNumber == 1)
        {
            selectedArray = act1EnemyCardsArray;
        }
        else if (actNumber == 2)
        {
            selectedArray = act2EnemyCardsArray;
        }
        else
        {
            selectedArray = act3EnemyCardsArray;
        }

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

    private bool ShouldDrawAllyEvent(int roundNumber, int actNumber)
    {
        if (allyDrawnInCurrentAct)
            return false;

        int currentAllyCount = RoundManager.instance.allyCardContainer.childCount;

        if (currentAllyCount >= 2)
            return false;

        if (roundNumber >= minRoundForAllyEvent && roundNumber <= maxRoundForAllyEvent)
        {
            if ((actNumber == 3 && currentAllyCount < 3) || (actNumber < 3 && currentAllyCount < actNumber))
            {
                return UnityEngine.Random.Range(0, 100) < allyEventProbability;
            }
        }

        if (roundNumber == maxRoundForAllyEvent + 1 && !allyDrawnInCurrentAct)
        {
            if ((actNumber == 3 && currentAllyCount < 3) || (actNumber < 3 && currentAllyCount < actNumber))
            {
                return true;
            }
        }

        return false;
    }

    private void DrawAllyEvent()
    {
        int randomIndex = UnityEngine.Random.Range(0, allyEventsArray.Length);
        GameObject eventCard = Instantiate(allyEventsArray[randomIndex], eventContainer);
        allyDrawnInCurrentAct = true;

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
