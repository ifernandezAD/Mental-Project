using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Canvas canvas;
    private BubbleDetector bubbleDetector;

    [Header("Drag Button")]
    private bool isReleased = false;
    private bool isBeingDragged = false;
    private RectTransform rectTransform;

    [Header("Bubble Multiplier")]
    private Vector3 originalScale;
    private int bubbleMultiplier = 1;
    private bool hasCombined = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isReleased = false;
        isBeingDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        Camera camera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, camera, out localPoint);

        Vector2 worldPoint = canvas.transform.TransformPoint(localPoint);
        Vector2 localPointInParent = rectTransform.parent.InverseTransformPoint(worldPoint);

        rectTransform.localPosition = localPointInParent;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isReleased = true;
        isBeingDragged = false;

        if (bubbleDetector != null)
        {
            bubbleDetector.CheckButtonType(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BubbleDetector>(out bubbleDetector))
        {
            // BubbleDetector encontrado, se puede usar posteriormente en OnPointerUp
        }

        if (other.CompareTag(gameObject.tag) && other.TryGetComponent<DraggableButton>(out DraggableButton otherBubble))
        {
            if (!otherBubble.hasCombined && !hasCombined)
            {
                if (isBeingDragged)
                {
                    CombineBubbles(otherBubble);
                }
            }
        }
    }

    private void OnTriggerStay2D()
    {
        if (isReleased && bubbleDetector != null)
        {
            bubbleDetector.CheckButtonType(this);
        }
    }

    private void CombineBubbles(DraggableButton otherBubble)
    {
        hasCombined = true;

        bubbleMultiplier += 1;

        rectTransform.localScale = originalScale * (1 + 0.2f * bubbleMultiplier);

        Destroy(otherBubble.gameObject);
    }
}
