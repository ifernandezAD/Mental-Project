using UnityEngine;

public class MultiplierBubble : Bubble
{
    public override void HandleCollision(Collider2D other)
    {
        if (other.TryGetComponent<Bubble>(out Bubble otherBubble))
        {
            if (other.CompareTag(gameObject.tag))
            {
                base.HandleCollision(other); 
            }
            else
            {
                IncreaseMultiplier(otherBubble);
                DestroyBubbleOnClone(); 
            }
        }
    }

    private void IncreaseMultiplier(Bubble otherBubble)
    {
        otherBubble.InitializeMultiplier(otherBubble.bubbleMultiplier + this.bubbleMultiplier); 
    }
}
