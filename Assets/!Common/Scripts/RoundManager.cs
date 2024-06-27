using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RoundManager : MonoBehaviour
{
    //Esta clase tendrá que tener un sistema con el orden de las rondas, cuando una de la señal de finalizar pasar a la siguiente, el mensaje podría ser un round finished

    public static RoundManager instance { get; private set; }

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

    private void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        Phase.onPhaseEnded += EnableNextPhase;
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

        //SetPhase(RoundPhase.Draw);
    }

    void UpdateUI()
    {
        roundText.text = $"Round: {currentRound} / {maxRoundsPerAct}";
        actText.text = $"Act: {currentAct} / {maxActs}";
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

    public IEnumerator DisplayText(string phaseName)
    {
        phaseText.text = phaseName;
        phaseText.gameObject.SetActive(true);
        yield return new WaitForSeconds(phaseTextDuration);
        phaseText.gameObject.SetActive(false);
    }

    public bool IsBossround()
    {
        return currentRound == maxRoundsPerAct;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    
    private void EnableNextPhase()
    {
        throw new NotImplementedException();
    }

    void OnDisable()
    {
        Phase.onPhaseEnded -= EnableNextPhase;
    }
}