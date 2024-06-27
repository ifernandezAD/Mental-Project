using System;
using UnityEngine;

public class EnemyPhase : Phase
{
    public static Action onEnemyPhase;
    protected override void BeginPhase()
    {
        onEnemyPhase?.Invoke();
    }
}
