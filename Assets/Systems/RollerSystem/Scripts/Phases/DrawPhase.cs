using System;
using UnityEngine;
using DG.Tweening;

public class DrawPhase : Phase
{
    [Header("Enemy Cards Variables")]
    [SerializeField] GameObject[] act1EnemyCardsArray;
    [SerializeField] GameObject[] act2EnemyCardsArray;
    [SerializeField] GameObject[] act3EnemyCardsArray;

    [Header("Boss Cards Variables")]
    [SerializeField] GameObject[] act1BossParts;
    [SerializeField] GameObject[] act2BossParts;
    [SerializeField] GameObject[] act3Boss0;
    [SerializeField] GameObject[] act3Boss1;
    [SerializeField] GameObject[] act3Boss2;
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

    [Header("Flashbacks")]
    [SerializeField] GameObject flashbackEventPopup;
    [SerializeField] private float flashbackProbability = 0.25f;

    protected override void InternalOnEnable()
    {
        base.InternalOnEnable();
        EventManager.onEventButtonPressed += OnEventButtonPressed;

        int currentRound = RoundManager.instance.GetCurrentRound();
        if (!Array.Exists(eventRounds, round => round == currentRound))
        {
            UIManagement.instance.OpenCurtain();
        }
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
                DrawGeneralEvent();
                return;
            }

            if (roundNumber == eventRounds[chosenAllyRound] && !allyDrawnInCurrentAct)
            {
                DrawAllyEvent();
                allyDrawnInCurrentAct = true;
            }
            else
            {
                DrawGeneralEvent();
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
        else if (enemyContainerUp.childCount == 0)
        {
            Instantiate(enemyCard, enemyContainerUp);
        }
        else if (enemyContainerDown.childCount == 0)
        {
            Instantiate(enemyCard, enemyContainerDown);
        }
        else
        {
            Debug.LogWarning("Todos los contenedores de enemigos están llenos.");
        }

        StartCoroutine(StartNextPhaseWithDelayCorroutine());
    }


    private void ClearEnemyCardContainer()
    {
        foreach (Transform child in enemyContainerFront)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in enemyContainerUp)
        {
            Destroy(child.gameObject);
        }
    }

    private void DrawBossCard()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        int actNumber = RoundManager.instance.GetCurrentAct();
        GameObject[] bossParts = actNumber switch
        {
            1 => act1BossParts,
            2 => act2BossParts,
            3 => GetAct3BossParts(selectedCharacterIndex),
            _ => null
        };

        if (bossParts != null)
        {
            if (bossParts.Length > 0)
                Instantiate(bossParts[0], enemyContainerFront);

            if (bossParts.Length > 1)
                Instantiate(bossParts[1], enemyContainerUp);

            if (bossParts.Length > 2)
                Instantiate(bossParts[2], enemyContainerDown);
        }

        StartCoroutine(StartNextPhaseWithDelayCorroutine());
    }

    private GameObject[] GetAct3BossParts(int index)
    {
        return index switch
        {
            0 => act3Boss0,
            1 => act3Boss1,
            2 => act3Boss2,
            _ => act3Boss0
        };
    }

    private void DrawAllyEvent()
    {
        DrawEventWithFlashback((bool isFlashback) =>
        {
            allyEventPopup.SetActive(true);
            allyEventPopup.GetComponent<GenericEvent>()?.Initialize(isFlashback);
            onEventTriggered?.Invoke();
        });
    }

    private void DrawGeneralEvent()
    {
        DrawEventWithFlashback((bool isFlashback) =>
        {
            int randomIndex = UnityEngine.Random.Range(0, eventsPopupArray.Length);
            GameObject selectedEvent = eventsPopupArray[randomIndex];
            selectedEvent.SetActive(true);
            selectedEvent.GetComponent<GenericEvent>()?.Initialize(isFlashback);
            onEventTriggered?.Invoke();
        });
    }

    private void OnEventButtonPressed()
    {
        if (enemyContainerFront.childCount == 0 && enemyContainerUp.childCount == 0 && enemyContainerDown.childCount == 0)
        {
            StartNextRoundWithDelay();
            UIManagement.instance.OpenCurtain();
            return;
        }
        StartCoroutine(StartNextPhaseWithDelayCorroutine());
        UIManagement.instance.OpenCurtain();
    }

    #region Flashbacks Management
    private void ShowFlashback()
    {
        flashbackEventPopup.SetActive(true);
        DOVirtual.DelayedCall(1f, () => { flashbackEventPopup.SetActive(false); });
    }

    private void DrawEventWithFlashback(Action<bool> eventAction)
    {
        bool isFlashback = ShouldTriggerFlashback();

        if (isFlashback)
        {
            ShowFlashback();
            DOVirtual.DelayedCall(1f, () => eventAction?.Invoke(true));
        }
        else
        {
            eventAction?.Invoke(false);
        }
    }
    private bool ShouldTriggerFlashback()
    {
        return UnityEngine.Random.value <= flashbackProbability;
    }
    public void ChangeProbability(float newProbability)
    {
        flashbackProbability = Mathf.Clamp01(newProbability);
    }
    #endregion

    protected override void InternalOnDisable()
    {
        base.InternalOnDisable();
        EventManager.onEventButtonPressed -= OnEventButtonPressed;
    }
}
