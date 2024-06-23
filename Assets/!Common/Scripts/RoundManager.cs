using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance { get; private set; }

    [Header("Feedback")]
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI phaseText;

    [Header("Round Variables")]
    [SerializeField] int maxRounds = 50;
    [SerializeField] int currentRound = 1;

    enum RoundPhase
    {
        Draw,
        Player,
        DamageResolution,
        Enemy,
        NewRound
    }
    RoundPhase currentPhase;
    private bool isRoundActive;

    [Header("Draw Phase")]
    [SerializeField] GameObject testingCardPrefab;
    [SerializeField] Transform cardContainer;

    [Header("Player Phase")]
    [SerializeField] Button rollButton;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        phaseText.gameObject.SetActive(false);

        StartRound();
 
        roundText.text = currentRound + " / " + maxRounds;
    }

    void StartRound()
    {
        if (currentRound <= maxRounds)
        {
            SetPhase(RoundPhase.Draw);
        }
        else
        {
            Debug.Log("Game Over. Total Rounds Completed: " + maxRounds);
        }
    }

    private void DrawPhase()
    {
        ShowPhaseText("Draw Phase");
        DrawEnemyCard();
        
        StartCoroutine(InvokeSetPhaseWithDelay(RoundPhase.Player, 2f));
    }

    void DrawEnemyCard()
    {
        GameObject card = Instantiate(testingCardPrefab, cardContainer);     
    }

    private void PlayerPhase()
    {
        ShowPhaseText("Player Phase");
        rollButton.interactable=true;
    }

    void ResolveDamage()
    {
        // Implement damage resolution logic
        Debug.Log("Resolving damage.");

        // Check if enemy is defeated or player is defeated
        bool enemyDefeated = false; // Replace with actual check
        bool playerDefeated = false; // Replace with actual check

        if (enemyDefeated)
        {
            SetPhase(RoundPhase.NewRound);
        }
        else if (playerDefeated)
        {
            Debug.Log("Game Over. Player defeated.");
            isRoundActive = false;
        }
        else
        {
            SetPhase(RoundPhase.Enemy);
        }
    }

    void EnemyAction()
    {
        // Implement enemy action logic
        Debug.Log("Enemy's turn.");
        SetPhase(RoundPhase.Player);
    }

    void StartNextRound()
    {
        isRoundActive = false;
        currentRound++;
        Debug.Log("Round " + currentRound + " ended.");
        StartRound();
    }

    public void OnEnemyDefeatedOrEventCompleted()
    {
        if (isRoundActive)
        {
            SetPhase(RoundPhase.DamageResolution);
        }
    }

    public void OnEndRoundButtonPressed()
    {
        if (isRoundActive && currentPhase == RoundPhase.Player)
        {
            SetPhase(RoundPhase.DamageResolution);
        }
    }

    private void ShowPhaseText(string phaseName)
    {
        StartCoroutine(DisplayText(phaseName));
    }

    private IEnumerator DisplayText(string phaseName)
    {
        phaseText.text = phaseName;
        phaseText.gameObject.SetActive(true); 
        yield return new WaitForSeconds(1);
        phaseText.gameObject.SetActive(false); 
    }

       private IEnumerator InvokeSetPhaseWithDelay(RoundPhase phase, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetPhase(phase);
    }

    
    void SetPhase(RoundPhase newPhase)
    {
        currentPhase = newPhase;
        switch (currentPhase)
        {
            case RoundPhase.Draw:
                DrawPhase();
                break;
            case RoundPhase.Player:
                PlayerPhase();
                break;
            case RoundPhase.DamageResolution:
                ResolveDamage();
                break;
            case RoundPhase.Enemy:
                EnemyAction();
                break;
            case RoundPhase.NewRound:
                StartNextRound();
                break;
        }
    }
}