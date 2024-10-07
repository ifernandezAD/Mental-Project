using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action onEventButtonPressed;

    public void EventButtonPressed()
    {
        onEventButtonPressed?.Invoke();
    }

}
