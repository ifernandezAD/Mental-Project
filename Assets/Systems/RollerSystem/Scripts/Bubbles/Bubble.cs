using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    [Header("Components")]
    private RectTransform rectTransform;

    [Header("Bubble Properties")]
    public int bubbleMultiplier { get; private set; } = 1;
    private bool hasCombined = false;
    private bool isCombining = false;
    private bool isBeingDragged = false;
    private bool canInteract = false;

    [Header("Multiplier UI")]
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private Image multiplierBackground;

    [Header("Growth Factor")]
    [SerializeField] private float growthFactor = 0.2f;

    [Header("Fusion Control")]
    [SerializeField] private float fusionCooldown = 0.5f;

    [Header("Particles")]
    [SerializeField] private GameObject cardParticleEffect;
    [SerializeField] private GameObject visuals;
    private bool fusionReady = true;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        InitializeMultiplierUI();
    }

    void Start()
    {
        fusionReady = true;
    }

    #region Setters
    public void SetBeingDragged(bool state)
    {
        isBeingDragged = state;
        canInteract = state;
    }

    public void SetFusionReady(bool state)
    {
        fusionReady = state;
    }
    #endregion

    #region Collision Handling
    public virtual void HandleCollision(Collider2D other)
    {
        if (other.TryGetComponent<Bubble>(out Bubble otherBubble) && canInteract)
        {
            if (other.CompareTag(gameObject.tag) && fusionReady && !hasCombined && isBeingDragged)
            {
                StartCoroutine(CombineBubbles(otherBubble));
            }
        }
    }

    private IEnumerator CombineBubbles(Bubble otherBubble)
    {
        isCombining = true;
        fusionReady = false;

        bubbleMultiplier += otherBubble.bubbleMultiplier;
        rectTransform.localScale = Vector3.one * (1 + growthFactor * bubbleMultiplier);

        UpdateMultiplierDisplay();
        otherBubble.DestroyBubble();

        yield return new WaitForSeconds(fusionCooldown);
        isCombining = false;
        fusionReady = true;
    }
    #endregion

    #region Bubble Management
    public void DestroyBubble()
    {
        hasCombined = true;
        Destroy(gameObject);
    }
    public void DestroyBubbleOnCardContact()
    {
        if (cardParticleEffect != null)
        {
            cardParticleEffect.SetActive(true);
        }

        visuals.SetActive(false);

        DOVirtual.DelayedCall(1f, () =>
   {
       Destroy(gameObject);
   });
    }

    public void InitializeMultiplier(int multiplier)
    {
        bubbleMultiplier = multiplier;
        rectTransform.localScale = Vector3.one * (1 + growthFactor * bubbleMultiplier);
        UpdateMultiplierDisplay();

        canInteract = false;
    }
    #endregion

    #region UI Updates
    private void InitializeMultiplierUI()
    {
        multiplierBackground.gameObject.SetActive(false);
        multiplierText.gameObject.SetActive(false);
    }

    public void UpdateMultiplierDisplay()
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
    #endregion
}
