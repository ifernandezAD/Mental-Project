using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action onEventButtonPressed;


    public void EventButtonPressed()
    {
        Debug.Log("Event Button Pressed");
        onEventButtonPressed?.Invoke();
    }

}
