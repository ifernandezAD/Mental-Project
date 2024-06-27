using System;
using System.Collections;
using UnityEngine;

public abstract class Phase : MonoBehaviour
{
    public static Action onPhaseEnded;

    [SerializeField] float phaseDelay = 2;
    [SerializeField] float nextPhaseDelay = 2;

    [SerializeField] string phaseName = "Phase";

    private void OnEnable() { InternalOnEnable(); }
    protected virtual void InternalOnEnable()
    {
        StartCoroutine(StartPhaseWithDelay());
        ShowPhaseText(phaseName);
    }

    private IEnumerator StartPhaseWithDelay()
    {

        yield return new WaitForSeconds(phaseDelay);
        BeginPhase();
    }

    protected void StartNextPhaseWithDelayCorroutine()
    {
        StartCoroutine(StartNextPhaseWithDelay());
    }

    protected IEnumerator StartNextPhaseWithDelay()
    {

        yield return new WaitForSeconds(nextPhaseDelay);
        RoundManager.instance.EnableNextPhase();
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
