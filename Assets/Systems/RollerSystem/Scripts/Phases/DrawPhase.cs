using System;
using UnityEngine;

public class DrawPhase : Phase
{
    [Header("Enemy Cards Variables")]
    [SerializeField] GameObject[] act1EnemyCardsArray;
    [SerializeField] GameObject[] act2EnemyCardsArray;
    [SerializeField] GameObject[] act3EnemyCardsArray;

    [Header("Boss Cards Variables")]
    [SerializeField] GameObject[] act1BossParts;
    [SerializeField] GameObject[] act2BossParts;
    [SerializeField] GameObject[] act3BossParts;
    [SerializeField] GameObject testingBossPrefab;

    [Header("Game Events Variables")]
    [SerializeField] GameObject[] eventsPopupArray;
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
        GameObject enemyCard = selectedArray[randomIndex];

        if (enemyContainerFront.childCount == 0)
        {
            Instantiate(enemyCard, enemyContainerFront);
        }

        else if (enemyContainerBack.childCount < 2)
        {
            Instantiate(enemyCard, enemyContainerBack);
        }
        else
        {
            Debug.LogWarning("Ambos contenedores de enemigos están llenos.");
        }

        StartCoroutine(StartNextPhaseWithDelayCorroutine());
    }

    private void ClearEnemyCardContainer()
    {
        foreach (Transform child in enemyContainerFront)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in enemyContainerBack)
        {
            Destroy(child.gameObject);
        }
    }

    private void DrawBossCard()
    {
        int actNumber = RoundManager.instance.GetCurrentAct();
        GameObject[] bossParts = actNumber switch
        {
            1 => act1BossParts,
            2 => act2BossParts,
            3 => act3BossParts,
            _ => null
        };

        if (bossParts != null)
        {
            Instantiate(bossParts[0], enemyContainerFront);

            for (int i = 1; i < bossParts.Length; i++)
            {
                Instantiate(bossParts[i], enemyContainerBack);
            }
        }

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
        int randomIndex = UnityEngine.Random.Range(0, eventsPopupArray.Length);
        GameObject selectedEvent = eventsPopupArray[randomIndex];
        selectedEvent.SetActive(true);

        onEventTriggered?.Invoke();
    }

    private void OnEventButtonPressed()
    {
        if (enemyContainerFront.childCount == 0 && enemyContainerBack.childCount == 0)
        {
            StartNextRoundWithDelay();
            return;
        }
        StartCoroutine(StartNextPhaseWithDelayCorroutine());
    }

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        EventManager.onEventButtonPressed -= OnEventButtonPressed;
    }
}
