using UnityEngine;

public class DrawPhase : Phase
{
    [SerializeField] GameObject testingCardPrefab;
    [SerializeField] GameObject testingBossPrefab;
    [SerializeField] Transform cardContainer;

    protected override void BeginPhase()
    {
        if (RoundManager.instance.IsBossround())
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
        GameObject card = Instantiate(testingCardPrefab, cardContainer);
        this.enabled=false;
    }

    private void DrawBossCard()
    {
        GameObject card = Instantiate(testingBossPrefab, cardContainer);
        this.enabled=false;
    }

    private void DrawEvent()
    {
        
    }

}
