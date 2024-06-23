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
    [SerializeField] int currentRound = 0;

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

    [Header("Damage Resolution")]

    public static Action onDamageResolution;

    private void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        OKButton.onOKButtonPressed += StartDamageResolutionPhase;
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
            currentRound++;
            roundText.text = currentRound + " / " + maxRounds;

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

    private void StartDamageResolutionPhase()
    {
        SetPhase(RoundPhase.DamageResolution);
    }

    void ResolveDamage()
    {
        ShowPhaseText("Damage Resolution");
        onDamageResolution?.Invoke();
    }

    public void StartEnemyActionPhase()
    {
        SetPhase(RoundPhase.Enemy);
    }

    void EnemyAction()
    {
        //El enemigo pega al Player y pasa lo que tenga que pasar.
        ShowPhaseText("Enemy Phase");
    }

    public void StartNextRound()
    {      
        currentRound++;
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

    void OnDisable()
    {
        OKButton.onOKButtonPressed -= StartDamageResolutionPhase;
    }
}