using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance { get; private set; }
    public static Action onEventButtonPressed;

    private void Awake()
    {
        instance = this;
    }
    public void EventButtonPressed()
    {
        onEventButtonPressed?.Invoke();
    }

}
