using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] GameObject testingCardPrefab;
    [SerializeField] Transform cardContainer;

    [Header("Round Variables")]
    [SerializeField] int totalRounds = 50;
    [SerializeField] int roundCounter = 1;
    [SerializeField] bool isRoundActive;

    void Start()
    {
        StartRound();
    }

    void StartRound()
    {
        if (roundCounter <= totalRounds)
        {
            isRoundActive = true;
            DrawEnemyCard();
        }
        else
        {
            Debug.Log("Game Over. Total Rounds Completed: " + totalRounds);
        }
    }

    void DrawEnemyCard()
    {
        GameObject card = Instantiate(testingCardPrefab, cardContainer);
        card.transform.localPosition = Vector3.zero;
        Debug.Log("Enemy card drawn.");
    }

    public void EndRound()
    {
        //Escucharemos al evento enemigoabatido o eventosuperado

        if (isRoundActive)
        {
            isRoundActive = false;
            Debug.Log("Round " + roundCounter + " ended.");
            roundCounter++;
            StartRound();
        }
    }

        public void OnEnemyDefeatedOrEventCompleted()
    {
        EndRound();
    }

    public void OnEndRoundButtonPressed()
    {
        EndRound();
    }

}
