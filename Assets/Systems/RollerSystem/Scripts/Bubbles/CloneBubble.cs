using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CloneBubble : Bubble
{
    private bool hasCloned = false;

    public override void HandleCollision(Collider2D other)
    {
        if (other.TryGetComponent<Bubble>(out Bubble otherBubble))
        {
            if (other.CompareTag(gameObject.tag))
            {
                base.HandleCollision(other);  
            }
            else if (!hasCloned)  
            {
                CloneOtherBubble(otherBubble.gameObject);
                DestroyBubbleWithEffect();  
                hasCloned = true;
            }
        }
    }

    private void CloneOtherBubble(GameObject otherBubble)
    {
        GameObject clonedBubble = Instantiate(otherBubble, transform.position, Quaternion.identity);
        Transform parentContainer = otherBubble.transform.parent;
        clonedBubble.transform.SetParent(parentContainer, false);

        Bubble clonedBubbleComponent = clonedBubble.GetComponent<Bubble>();
        if (clonedBubbleComponent != null)
        {
            int originalMultiplier = otherBubble.GetComponent<Bubble>().bubbleMultiplier;
            if (originalMultiplier > 1)  
            {
                clonedBubbleComponent.InitializeMultiplier(originalMultiplier);
            }

            clonedBubbleComponent.SetFusionReady(false); 
            StartCoroutine(EnableInteractionAfterDelay(clonedBubbleComponent, 0.5f));  
        }

        BubblesManager.instance.EnableLayout();
        DOVirtual.DelayedCall(0.5f, BubblesManager.instance.DisableLayout);
    }

    private IEnumerator EnableInteractionAfterDelay(Bubble clonedBubble, float delay)
    {
        yield return new WaitForSeconds(delay);
        clonedBubble.SetFusionReady(true);  
        clonedBubble.SetBeingDragged(false);  
    }
}
