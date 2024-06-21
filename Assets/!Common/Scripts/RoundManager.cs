using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] int totalRounds = 50;
    [SerializeField] int roundCounter = 1;
    [SerializeField] bool isRoundActive;

    void Start()
    {

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
