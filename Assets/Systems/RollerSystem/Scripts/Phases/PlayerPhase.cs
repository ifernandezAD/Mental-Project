using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPhase : Phase
{
    //This class has to found enemy cards and activate his takedamage methods
    public static Action onDamageResolution;
    protected override void BeginPhase()
    {
        onDamageResolution?.Invoke();
    }
}
