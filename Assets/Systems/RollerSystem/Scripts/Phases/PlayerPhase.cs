using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPhase : Phase
{
    public static Action onDamageResolution;
    protected override void BeginPhase()
    {
        onDamageResolution?.Invoke();

        this.enabled = false;
    }
}
