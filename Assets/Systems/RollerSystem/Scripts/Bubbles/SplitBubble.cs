using UnityEngine;
using DG.Tweening;

public class SplitBubble : Bubble
{
    public override void HandleCollision(Collider2D other)
    {
        if (other.TryGetComponent<Bubble>(out Bubble otherBubble))
        {
            if (other.CompareTag(gameObject.tag))
            {
                base.HandleCollision(other); 
            }
            else if (otherBubble.bubbleMultiplier > 1) 
            {
                SplitBubbleIntoBasics(otherBubble);
                otherBubble.DestroyBubble(); 
                DestroyBubble();
            }
        }
    }

    private void SplitBubbleIntoBasics(Bubble otherBubble)
    {
        int originalMultiplier = otherBubble.bubbleMultiplier;

        Transform parentContainer = otherBubble.transform.parent;

        for (int i = 0; i < originalMultiplier; i++)
        {
            GameObject basicBubble = Instantiate(otherBubble.gameObject, transform.position, Quaternion.identity);
            Bubble basicBubbleComponent = basicBubble.GetComponent<Bubble>();

            basicBubbleComponent.InitializeMultiplier(1);
            basicBubbleComponent.SetFusionReady(false); 

            basicBubble.transform.SetParent(parentContainer, false);
            
            basicBubbleComponent.transform.localScale = Vector3.one;
        }

        BubblesManager.instance.EnableLayout();
        DOVirtual.DelayedCall(0.5f, BubblesManager.instance.DisableLayout);
    }
}
