using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{ 
    [Header("Display")]
    public string eventName;
    public Sprite art;

    [Header("Buttons")]

    public string firstButtonText;
    public string secondButtonText;
    public string thirdButtonText;
}
