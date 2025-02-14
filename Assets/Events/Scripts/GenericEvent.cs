using UnityEngine;

public class GenericEvent : MonoBehaviour
{
    public bool IsFlashback { get; private set; }

    public void Initialize(bool isFlashback)
    {
        IsFlashback = isFlashback;
    }
}
