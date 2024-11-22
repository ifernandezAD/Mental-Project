using UnityEngine;

public class DestroyBubble : Bubble
{
    public override void HandleCollision(Collider2D other)
    {
        if (other.TryGetComponent<Bubble>(out Bubble otherBubble))
        {
            DestroyBubbleWithEffect(); 
            otherBubble.DestroyBubble();
        }
    }
}
