using UnityEngine;

public class DrawPhase : Phase
{
    [Header("Enemy Cards Variables")]
    [SerializeField] GameObject testingCardPrefab;
    [SerializeField] GameObject testingBossPrefab;

    [Header("Game Events Variables")]
    [SerializeField] GameObject[] generalEventsArray;
    [SerializeField] GameObject[] allyEventsArray;
    [SerializeField] Transform eventContainer;
    [SerializeField, Range(0, 100)] private int eventDrawProbability = 20;
    private bool allyDrawnInCurrentAct = false;


    protected override void InternalOnEnable()
    {
        base.InternalOnEnable();
        EventManager.onEventButtonPressed += OnEventButtonPressed;
    }

    protected override void BeginPhase()
    {
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
        int roundNumber = RoundManager.instance.GetCurrentRound();
        int actNumber = RoundManager.instance.GetCurrentAct();

        if (ShouldDrawAllyEvent(roundNumber, actNumber))
        {
            DrawAllyEvent();
        }
        else
        {
            DrawGeneralEvent();
        }
    }

        private bool ShouldDrawAllyEvent(int roundNumber, int actNumber)
    {
        if (allyDrawnInCurrentAct)
            return false;

        if (roundNumber >= 3 && roundNumber <= 8)
        {
            int currentAllyCount = RoundManager.instance.allyCardContainer.childCount;
            if (actNumber == 3 && currentAllyCount < 3)
            {
                return true;
            }
            else if (actNumber < 3 && currentAllyCount < actNumber)
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
