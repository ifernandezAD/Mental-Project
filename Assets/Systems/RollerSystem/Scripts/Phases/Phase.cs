using System;
using System.Collections;
using UnityEngine;

public abstract class Phase : MonoBehaviour
{
    public static Action onPhaseEnded;

    [Header("References")]
    protected Transform enemyCardContainer;
    protected Transform characterCardContainer;
    protected Transform allyCardContainer;

    [SerializeField] float phaseDelay = 2;
    [SerializeField] float nextPhaseDelay = 2;

    [SerializeField] string phaseName = "Phase";

    private void OnEnable() { InternalOnEnable(); }
    protected virtual void InternalOnEnable()
    {
        StartCoroutine(StartPhaseWithDelay());
        ShowPhaseText(phaseName);
    }

    private void Awake() { InternalAwake(); }

    protected virtual void InternalAwake()
    {
        enemyCardContainer = RoundManager.instance.enemyCardContainer;
        characterCardContainer = RoundManager.instance.characterCardContainer;
        allyCardContainer = RoundManager.instance.allyCardContainer;
    }

    private IEnumerator StartPhaseWithDelay()
    {
        yield return new WaitForSeconds(phaseDelay);
        BeginPhase();
    }

    protected void StartNextPhaseWithDelay()
    {
        StartCoroutine(StartNextPhaseWithDelayCorroutine());
    }

    protected IEnumerator StartNextPhaseWithDelayCorroutine()
    {

        yield return new WaitForSeconds(nextPhaseDelay);
        RoundManager.instance.EnableNextPhase();
    }

    protected void StartRollerPhaseWithDelay()
    {
        StartCoroutine(StartRollerPhaseWithDelayCorroutine());
    }

    protected IEnumerator StartRollerPhaseWithDelayCorroutine()
    {
        yield return new WaitForSeconds(nextPhaseDelay);
        RoundManager.instance.EnableRollerPhase();
    }

    
    protected void StartNextRoundWithDelay()
    {
        StartCoroutine(StartNextRoundWithDelayCorroutine());
    }

    protected IEnumerator StartNextRoundWithDelayCorroutine()
    {
        yield return new WaitForSeconds(nextPhaseDelay);
        RoundManager.instance.StartNextRound();
    }
    protected void StartNextActWithDelay()
    {
        StartCoroutine(StartNextActWithDelayCorroutine());
    }

    protected IEnumerator StartNextActWithDelayCorroutine()
    {
        yield return new WaitForSeconds(nextPhaseDelay);
        RoundManager.instance.StartNextAct();
    }

    private void ShowPhaseText(string phaseName)
    {
        StartCoroutine(RoundManager.instance.DisplayText(phaseName));
    }

    protected abstract void BeginPhase();

    private void OnDisable() { InternalOnDisable(); }
    protected virtual void InternalOnDisable()
    {

    }
}
