using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isReleased = false;
    private RectTransform rectTransform;
    private Canvas canvas;

    private CardDetector cardDetector;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isReleased=false;
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
        isReleased=true;
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
         cardDetector = other.GetComponent<CardDetector>();   
    }

   private void OnTriggerStay2D()
   {
        if(isReleased)
        {
            cardDetector.CheckButtonType(this);
        }
   }
}
