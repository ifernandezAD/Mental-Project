using System;
using UnityEngine;

public class EventSimulation : MonoBehaviour
{
    private const int numActs = 3;
    private const int roundsPerAct = 10;
    private const int bossRound = 10;
    [SerializeField] int normalEventProbability = 30;
    [SerializeField] private int allyEventProbability = 30;
    private int minRoundForAllyEvent = 3;
    private int maxRoundForAllyEvent = 8;

    private int normalEventCount = 0;
    private int maxNormalEventsPerAct = 3;
    private int allyEventCount = 0;
    private int maxAllies = 2;
    private bool lastDrawWasEvent = false;
    private bool allyDrawnInCurrentAct = false;

    void Start()
    {
        Simulate();
    }
    public void Simulate()
    {
        for (int act = 1; act <= numActs; act++)
        {
            Debug.Log($"Simulando acto {act}");
            normalEventCount = 0;
            allyDrawnInCurrentAct = false;

            for (int round = 1; round <= roundsPerAct; round++)
            {
                if (round == bossRound)
                {
                    Debug.Log($"Ronda {round}: Boss, no hay evento.");
                    continue;
                }

                if (round <= 2)
                {
                    // Las primeras dos rondas no tienen eventos
                    DrawEnemy(act, round);
                }
                else
                {
                    if (lastDrawWasEvent)
                    {
                        DrawEnemy(act, round);
                        lastDrawWasEvent = false;
                    }
                    else
                    {
                        if (normalEventCount < maxNormalEventsPerAct && UnityEngine.Random.Range(0, 100) < normalEventProbability)
                        {
                            DrawNormalEvent(act, round);
                        }
                        else if (ShouldDrawAllyEvent(round, act))
                        {
                            DrawAllyEvent(act, round);
                        }
                        else
                        {
                            DrawEnemy(act, round);
                        }
                    }
                }
            }
        }
    }

    private bool ShouldDrawAllyEvent(int round, int act)
    {
        if (allyDrawnInCurrentAct || allyEventCount >= maxAllies)
            return false;

        if (round >= minRoundForAllyEvent && round <= maxRoundForAllyEvent)
        {
            if (act < 3 || (act == 3 && allyEventCount < 2))
            {
                return UnityEngine.Random.Range(0, 100) < allyEventProbability;
            }
        }

        return false;
    }

    private void DrawNormalEvent(int act, int round)
    {
        normalEventCount++;
        lastDrawWasEvent = true;
        Debug.Log($"Ronda {round} del acto {act}: Evento normal (evento {normalEventCount} del acto)");
    }

    private void DrawAllyEvent(int act, int round)
    {
        allyDrawnInCurrentAct = true;
        allyEventCount++;
        lastDrawWasEvent = true;
        Debug.Log($"Ronda {round} del acto {act}: Evento de aliado (aliado {allyEventCount} total)");
    }

    private void DrawEnemy(int act, int round)
    {
        Debug.Log($"Ronda {round} del acto {act}: Se dibuja una carta de enemigo.");
    }
}
