using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

[DefaultExecutionOrder(-100)]
public class RoundManager : MonoBehaviour
{
    public static RoundManager instance { get; private set; }

    [Header("References")]
    [SerializeField] public Transform enemyContainerFront; 
    [SerializeField] public Transform enemyContainerBack; 
    [SerializeField] public Transform characterCardContainer;
    [SerializeField] public Transform allyCardContainer;


    [Header("Feedback")]
    [SerializeField] TextMeshProUGUI actText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI phaseText;
    [SerializeField] float phaseTextDuration;

    [Header("Round Variables")]
    [SerializeField] int maxRoundsPerAct = 15;
    [SerializeField] int currentRound = 0;
    [SerializeField] int currentAct = 1;
    [SerializeField] int maxActs = 3;

    [Header("Phases")]
    [SerializeField] public Phase[] phases;
    public int currentPhaseIndex = 0;

    private void Awake()
    {
        instance = this;

        phases = GetComponentsInChildren<Phase>();
        foreach (var phase in phases)
        {
            phase.enabled = false;
        }
    }

    void Start()
    {
        phaseText.gameObject.SetActive(false);

        SolveActOneMentalHealthEffects();
        StartRound();
    }

    void SolveActOneMentalHealthEffects()
    {
        GameLogic.instance.mainCharacterCard.GetComponent<MentalHealthEffects>().TriggerFirstActMentalHealthEffects();
    }

    void SolveActTwoMentalHealthEffects()
    {
        GameLogic.instance.mainCharacterCard.GetComponent<MentalHealthEffects>().TriggerSecondActMentalHealthEffects();
    }

    void SolveActThreeMentalHealthEffects()
    {
        GameLogic.instance.mainCharacterCard.GetComponent<MentalHealthEffects>().TriggerThirdActMentalHealthEffects();
    }

    void StartRound()
    {
        currentPhaseIndex = 0;
        currentRound++;

        UpdateUI();
        StartCurrentPhase();
    }

    void UpdateUI()
    {
        roundText.text = $"Round: {currentRound} / {maxRoundsPerAct}";
        actText.text = $"Act: {currentAct} / {maxActs}";
    }

    private void StartCurrentPhase()
    {
        if (currentPhaseIndex < phases.Length)
        {
            phases[currentPhaseIndex].enabled = true;
        }
    }

    public void EnableNextPhase()
    {
        if (currentPhaseIndex < phases.Length)
        {
            phases[currentPhaseIndex].enabled = false;
        }

        currentPhaseIndex++;
        if (currentPhaseIndex < phases.Length)
        {
            StartCurrentPhase();
        }
        else
        {
            StartNextRound();
        }
    }

    public void EnableRollerPhase()
    {
        if (currentPhaseIndex < phases.Length)
        {
            phases[currentPhaseIndex].enabled = false;
        }

        currentPhaseIndex =1;

        StartCurrentPhase();
    }

    public void StartNextRound()
    {
        if (currentPhaseIndex < phases.Length)
        {
            phases[currentPhaseIndex].enabled = false;
        }

        StartRound();
    }

    public void StartNextAct()
    {
        phases[currentPhaseIndex].enabled = false;

        if (currentAct < maxActs)
        {
            currentAct++;
            currentRound = 0;

            UIManagement.instance.SetActiveActImage(currentAct - 1);

            if(currentAct==2)
            {
                SolveActTwoMentalHealthEffects();
            }

            if(currentAct==3)
            {
                SolveActThreeMentalHealthEffects();
            }

            StartRound();
        }
        else
        {
            Debug.Log("CONGRATULATIONS!!!! YOU WON THE GAME");
        }
    }

    public IEnumerator DisplayText(string phaseName)
    {
        phaseText.text = phaseName;
        phaseText.gameObject.SetActive(true);
        yield return new WaitForSeconds(phaseTextDuration);
        phaseText.gameObject.SetActive(false);
    }

    public bool IsBossRound()
    {
        return currentRound == maxRoundsPerAct;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    public int GetCurrentRound()
    {
       return currentRound;
    }

    public int GetCurrentAct()
    {
        return currentAct;
    }
}