using UnityEngine;

public class GenericEvent : MonoBehaviour
{
    public bool IsFlashback { get; private set; }
    public bool IsGoodFlashback { get; private set; } 

    public void Initialize(bool isFlashback)
    {
        IsFlashback = isFlashback;

        if (IsFlashback)
        {
            IsGoodFlashback = Random.value < 0.5f; 
            Debug.Log($"Evento de flashback inicializado. Es flashback: {IsFlashback}, Es bueno: {IsGoodFlashback}");
        }
        else
        {
            Debug.Log($"Evento normal inicializado.");
        }
    }
}
