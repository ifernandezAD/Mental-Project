using UnityEngine;

public class GenericEvent : MonoBehaviour
{
    public bool IsFlashback { get; private set; }
    public bool IsGoodFlashback { get; private set; } 

    public virtual void Initialize(bool isFlashback)
    {
        IsFlashback = isFlashback;

        if (IsFlashback)
        {
            IsGoodFlashback = Random.value < 0.1f; //Cambiar luego a 0.5, testing ahora
            Debug.Log($"Evento de flashback inicializado. Es flashback: {IsFlashback}, Es bueno: {IsGoodFlashback}");
        }
        else
        {
            Debug.Log($"Evento normal inicializado.");
        }
    }
}
