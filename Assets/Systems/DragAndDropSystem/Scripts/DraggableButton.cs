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
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        if(canvas ==null){canvas = GetComponentInParent<Canvas>();}

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

        if (bubbleDetector != null)
        {
            bubbleDetector.CheckButtonType(this, bubble.bubbleMultiplier);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BubbleDetector>(out bubbleDetector))
        {
            // BubbleDetector encontrado, se puede usar posteriormente en OnPointerUp
        }

        bubble.HandleCollision(other);
    }

    private void OnTriggerStay2D()
    {
        if (isReleased && bubbleDetector != null)
        {
            bubbleDetector.CheckButtonType(this, bubble.bubbleMultiplier);
        }
    }
}
