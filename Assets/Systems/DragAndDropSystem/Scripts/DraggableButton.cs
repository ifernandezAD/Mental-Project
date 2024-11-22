using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Canvas canvas;
    private BubbleDetector bubbleDetector;
    private RectTransform rectTransform;

    private Bubble bubble;

    private bool isReleased = false;
    private bool isBeingDragged = false;

    private bool isOverBubbleDetector = false; // Nueva variable para saber si estamos sobre un BubbleDetector

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        bubble = GetComponent<Bubble>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isReleased = false;
        isBeingDragged = true;
        bubble.SetBeingDragged(true);

        // Resetear estado al inicio del arrastre
        isOverBubbleDetector = false;
        bubbleDetector = null; // Limpiar la referencia del detector
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        Camera camera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, camera, out localPoint))
        {
            rectTransform.localPosition = localPoint;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isReleased = true;
        isBeingDragged = false;
        bubble.SetBeingDragged(false);

        // Solo resolver el efecto si la burbuja fue soltada sobre un BubbleDetector
        if (isOverBubbleDetector && bubbleDetector != null)
        {
            bubbleDetector.CheckButtonType(this, bubble.bubbleMultiplier);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BubbleDetector>(out bubbleDetector))
        {
            // Establecemos que estamos sobre un BubbleDetector
            isOverBubbleDetector = true;
            // Aseguramos que solo se mantenga la referencia al primer detector
            if (bubbleDetector != null)
            {
                bubbleDetector = other.GetComponent<BubbleDetector>();
            }
        }

        bubble.HandleCollision(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<BubbleDetector>(out bubbleDetector))
        {
            // Si salimos del BubbleDetector, limpiamos la referencia
            isOverBubbleDetector = false;
            bubbleDetector = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Aquí actualizamos la referencia si estamos sobre un BubbleDetector
        if (other.TryGetComponent<BubbleDetector>(out BubbleDetector newDetector))
        {
            if (newDetector != bubbleDetector)
            {
                // Si estamos sobre un nuevo detector, actualizamos la referencia
                bubbleDetector = newDetector;
                isOverBubbleDetector = true;
            }
        }

        // Solo se debe verificar la acción si la burbuja fue soltada sobre un BubbleDetector
        if (isReleased && isOverBubbleDetector && bubbleDetector != null)
        {
            bubbleDetector.CheckButtonType(this, bubble.bubbleMultiplier);
        }
    }
}
