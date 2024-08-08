using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action onEventButtonPressed;
    public static Action onAllyObtained;

    public void EventButtonPressed()
    {
        onEventButtonPressed?.Invoke();
    }

    public void AllyObtained()
    {
        onAllyObtained?.Invoke();
    }
}
