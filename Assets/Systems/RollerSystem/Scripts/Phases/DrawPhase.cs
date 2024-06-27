using UnityEngine;

public class DrawPhase : Phase
{
    [SerializeField] GameObject testingCardPrefab;
    [SerializeField] GameObject testingBossPrefab;
    [SerializeField] Transform enemyCardContainer;

    protected override void BeginPhase()
    {
        if (RoundManager.instance.IsBossRound())
        {
            DrawBossCard();
        }
        else
        {
            DrawEnemyCard(); //TO DO , método que calcule de forma aleatoria si robamos carta de enemigo o evento
        }
    }

    private void DrawEnemyCard()
    {
        GameObject card = Instantiate(testingCardPrefab, enemyCardContainer);
        StartCoroutine(StartNextPhaseWithDelay());
    }

    private void DrawBossCard()
    {
        GameObject card = Instantiate(testingBossPrefab, enemyCardContainer);    
        StartCoroutine(StartNextPhaseWithDelay());
    }

    private void DrawEvent()
    {
        
    }

}
