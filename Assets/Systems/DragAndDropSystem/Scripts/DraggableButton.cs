using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DraggableButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Canvas canvas;
    private BubbleDetector bubbleDetector;

    [Header("Drag Button")]
    private bool isReleased = false;
    private bool isBeingDragged = false;
    private RectTransform rectTransform;

    [Header("Bubble Multiplier Logic")]
    private Vector3 originalScale;
    public int bubbleMultiplier {get;private set;} = 1;
    private bool hasCombined = false;

    [Header("Multiplier UI")]
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] Image multiplierBackground;

    [Header("Growth Factor")]
    [SerializeField] private float growthFactor = 0.2f;  

    [Header("Fusion Control")]
    [SerializeField] private float fusionCooldown = 0.5f;  

    private bool fusionReady = true;  

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;

        multiplierBackground.gameObject.SetActive(false);
        multiplierText.gameObject.SetActive(false);
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
            bubbleDetector.CheckButtonType(this, bubbleMultiplier);
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
            if (fusionReady && !otherBubble.hasCombined && isBeingDragged)
            {
                StartCoroutine(CombineBubbles(otherBubble));
            }
        }
    }

    private IEnumerator CombineBubbles(DraggableButton otherBubble)
    {
        otherBubble.hasCombined = true;
        fusionReady = false;

        bubbleMultiplier += 1;

        rectTransform.localScale = originalScale * (1 + growthFactor * bubbleMultiplier);

        UpdateMultiplierDisplay();
        Destroy(otherBubble.gameObject);

        yield return new WaitForSeconds(fusionCooldown);

        fusionReady = true;
    }

    private void OnTriggerStay2D()
    {
        if (isReleased && bubbleDetector != null)
        {
            bubbleDetector.CheckButtonType(this, bubbleMultiplier);
        }
    }

    private void UpdateMultiplierDisplay()
    {
        if (bubbleMultiplier >= 2)
        {
            multiplierBackground.gameObject.SetActive(true);
            multiplierText.gameObject.SetActive(true);
            multiplierText.text = "x" + bubbleMultiplier;
        }
        else
        {
            multiplierBackground.gameObject.SetActive(false);
            multiplierText.gameObject.SetActive(false);
        }
    }
}
