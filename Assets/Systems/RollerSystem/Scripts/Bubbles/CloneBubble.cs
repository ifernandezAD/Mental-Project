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
                base.HandleCollision(other);  // Comportamiento normal de fusión si son del mismo tipo
            }
            else if (!hasCloned)  // Solo clonar una vez
            {
                CloneOtherBubble(otherBubble.gameObject);
                DestroyBubble();  // Destruir la CloneBubble después de clonar
                hasCloned = true;
            }
        }
    }

    private void CloneOtherBubble(GameObject otherBubble)
    {
        // Crear una copia exacta del GameObject con el que choca la CloneBubble
        GameObject clonedBubble = Instantiate(otherBubble, transform.position, Quaternion.identity);

        // Obtener el parent (contenedor) de la burbuja con la que colisionamos
        Transform parentContainer = otherBubble.transform.parent;
        
        // Asegurarnos de que la burbuja clonada esté dentro del mismo contenedor
        clonedBubble.transform.SetParent(parentContainer, false);

        // Obtener el componente Bubble del objeto clonado y transferir el multiplicador solo si es mayor que 1
        Bubble clonedBubbleComponent = clonedBubble.GetComponent<Bubble>();
        if (clonedBubbleComponent != null)
        {
            int originalMultiplier = otherBubble.GetComponent<Bubble>().bubbleMultiplier;
            if (originalMultiplier > 1)  // Solo ajustar si el multiplicador original es mayor que 1
            {
                clonedBubbleComponent.InitializeMultiplier(originalMultiplier);
            }

            clonedBubbleComponent.SetFusionReady(false);  // Desactivar temporalmente la fusión
            StartCoroutine(EnableInteractionAfterDelay(clonedBubbleComponent, 0.5f));  // Habilitar interacción después de un delay
        }

        BubblesManager.instance.EnableLayout();
        DOVirtual.DelayedCall(0.5f, BubblesManager.instance.DisableLayout);
    }

    private IEnumerator EnableInteractionAfterDelay(Bubble clonedBubble, float delay)
    {
        yield return new WaitForSeconds(delay);
        clonedBubble.SetFusionReady(true);  // Reactivar fusión
        clonedBubble.SetBeingDragged(false);  // Asegurar que la burbuja se pueda arrastrar y no interactúe automáticamente
    }
}
