using UnityEngine;

public class DrawPhase : Phase
{
    [Header("Enemy Cards Variables")]    
    [SerializeField] GameObject testingCardPrefab;
    [SerializeField] GameObject testingBossPrefab;

    [Header("Game Events Variables")]
    [SerializeField] GameObject[] gameEventsArray;
    [SerializeField] Transform eventContainer;
    [SerializeField, Range(0, 100)] private int eventDrawProbability = 20;


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
        int randomIndex = Random.Range(0, gameEventsArray.Length);
        GameObject eventCard = Instantiate(gameEventsArray[randomIndex], eventContainer);
    }

    private void OnEventButtonPressed()
    {
        ClearEventContainer();
        StartCoroutine(StartNextPhaseWithDelayCorroutine()); //Tenemos que checkear si hay enemigo o no, en el caso de que no se inicia una nueva drawphase
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
