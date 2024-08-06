using UnityEngine;

public class DrawPhase : Phase
{
    [Header("Enemy Cards Variables")]
    [SerializeField] GameObject testingCardPrefab;
    [SerializeField] GameObject testingBossPrefab;

    [Header("Game Events Variables")]
    [SerializeField] GameObject[] generalEventsArray;
    [SerializeField] Transform eventContainer;

    [Header("Ally Events Variables")]
    [SerializeField] GameObject[] allyEventsArray;
    [SerializeField, Range(0, 100)] private int eventDrawProbability = 20;
    [SerializeField] private int minRoundForAllyEvent = 3;
    [SerializeField] private int maxRoundForAllyEvent = 8;
    [SerializeField, Range(0, 100)] private int allyEventProbability = 50;
    private bool allyDrawnInCurrentAct = false;



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
            int randomValue = Random.Range(0, 100);

            if (randomValue < eventDrawProbability)
            {
                DrawEvent();
            }
            else
            {
                DrawEnemyCard();
            }
        }
    }

    private void DrawEnemyCard()
    {
        GameObject card = Instantiate(testingCardPrefab, enemyCardContainer);
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

        
        if (roundNumber >= minRoundForAllyEvent && roundNumber <= maxRoundForAllyEvent)
        {
            if ((actNumber == 3 && currentAllyCount < 3) || (actNumber < 3 && currentAllyCount < actNumber))
            {
                return Random.Range(0, 100) < allyEventProbability;
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
        int randomIndex = Random.Range(0, allyEventsArray.Length);
        GameObject eventCard = Instantiate(allyEventsArray[randomIndex], eventContainer);
        allyDrawnInCurrentAct = true;
    }

    private void DrawGeneralEvent()
    {
        int randomIndex = Random.Range(0, generalEventsArray.Length);
        GameObject eventCard = Instantiate(generalEventsArray[randomIndex], eventContainer);
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
