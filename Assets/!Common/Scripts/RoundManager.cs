using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance { get; private set; }

    [Header("Feedback")]
    [SerializeField] TextMeshProUGUI actText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI phaseText;

    [Header("Round Variables")]
    [SerializeField] int maxRoundsPerAct = 15;
    [SerializeField] int currentRound = 0;
    [SerializeField] int currentAct = 1;
    [SerializeField] int maxActs = 3;

    enum RoundPhase
    {
        Draw,
        Player,
        DamageResolution,
        Enemy,
        NewRound
    }
    RoundPhase currentPhase;

    [Header("Draw Phase")]
    [SerializeField] GameObject testingCardPrefab;
    [SerializeField] GameObject testingBossPrefab;
    [SerializeField] Transform cardContainer;

    [Header("Player Phase")]
    [SerializeField] Button rollButton;

    [Header("Damage Resolution")]
    public static Action onDamageResolution;

    [Header("Enemy Phase")]
    public static Action onEnemyPhase;

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
    }

    void StartRound()
    {
        currentRound++;
        UpdateUI();

        SetPhase(RoundPhase.Draw);
    }

    void UpdateUI()
    {
        roundText.text = $"Round: {currentRound} / {maxRoundsPerAct}";
        actText.text = $"Act: {currentAct} / {maxActs}";
    }

    private void DrawPhase()
    {
        ShowPhaseText("Draw Phase");

        if (currentRound < maxRoundsPerAct)
        {
            DrawEnemyCard();
            //Or draw an event

            StartCoroutine(InvokeSetPhaseWithDelay(RoundPhase.Player, 2f));
        }
        else
        {
            DrawBossCard();

            StartCoroutine(InvokeSetPhaseWithDelay(RoundPhase.Player, 2f));
        }
    }


    void DrawEnemyCard()
    {
        GameObject card = Instantiate(testingCardPrefab, cardContainer);
    }

    private void DrawBossCard()
    {
        GameObject card = Instantiate(testingBossPrefab, cardContainer);
    }

    public void StartPlayerPhase()
    {
        StartCoroutine(InvokeSetPhaseWithDelay(RoundPhase.Player, 2f));
    }

    private void PlayerPhase()
    {
        ShowPhaseText("Player Phase");
        ResetRoller();
    }

    private void ResetRoller()
    {
        rollButton.interactable = true;
        Roller.instance.ResetEnergy();
        Roller.instance.DisableAllSlotImages();
        Roller.instance.UnlockAllSlots();
    }

    private void StartDamageResolutionPhase()
    {

        StartCoroutine(InvokeSetPhaseWithDelay(RoundPhase.DamageResolution, 2f));
    }

    void ResolveDamage()
    {
        ShowPhaseText("Damage Resolution");
        onDamageResolution?.Invoke();
    }

    public void StartEnemyActionPhase()
    {
        StartCoroutine(InvokeSetPhaseWithDelay(RoundPhase.Enemy, 2f));
    }

    void EnemyAction()
    {
        ShowPhaseText("Enemy Phase");
        onEnemyPhase?.Invoke();
    }

    public void StartNextRound()
    {
        StartRound();
    }

    public void StartNextAct()
    {
        if (currentAct < maxActs)
        {
            currentAct++;
            currentRound = 0;

            StartRound();
            
        //Here we launch the event to worse mental disease and increase difficulty
        }
        else
        {
            Debug.Log("CONGRATULATIONS!!!! YOU WON THE GAME");
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

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    void OnDisable()
    {
        OKButton.onOKButtonPressed -= StartDamageResolutionPhase;
    }
}