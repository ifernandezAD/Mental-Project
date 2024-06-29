using UnityEngine;

public class CardDetector : MonoBehaviour
{
    public void HandleButtonRelease(DraggableButton button)
    {
        Debug.Log("Button released over the card!");
        Destroy(button.gameObject);
    }
}