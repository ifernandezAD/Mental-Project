using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isDragging = false;
    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 localPoint;
            // Use the appropriate camera for the canvas
            Camera camera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

            // Convert the screen point to local point in the canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, camera, out localPoint);

            // Convert the local point in the canvas to world point
            Vector2 worldPoint = canvas.transform.TransformPoint(localPoint);

            // Convert the world point to local point in the parent of the button
            Vector2 localPointInParent = rectTransform.parent.InverseTransformPoint(worldPoint);

            // Assign the new local position to the button
            rectTransform.localPosition = localPointInParent;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CardDetector cardDetector = other.GetComponent<CardDetector>();
        if (cardDetector != null)
        {
            cardDetector.HandleButtonRelease(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Optionally handle trigger exit events if needed
    }
}
