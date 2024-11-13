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
    private bool fusionReady = true;

    [Header("Particles")]
    [SerializeField] private GameObject particleContainer;
    [SerializeField] private GameObject visuals;
    [SerializeField] private Collider2D bubbleCollider;
    private GameObject cloneParticleEffect;
    private GameObject inGameParticleEffect;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        InitializeMultiplierUI();
    }

    void Start()
    {
        fusionReady = true;
        cloneParticleEffect = particleContainer.transform.GetChild(0).gameObject;
        inGameParticleEffect = particleContainer.transform.GetChild(1).gameObject;
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
        StartCoroutine(ActivateCloneParticleEffect());

        yield return new WaitForSeconds(fusionCooldown);
        isCombining = false;
        fusionReady = true;
    }

    private IEnumerator ActivateCloneParticleEffect()
    {
        if (cloneParticleEffect != null)
        {
            cloneParticleEffect.transform.localScale = Vector3.zero;
            cloneParticleEffect.SetActive(true);

            cloneParticleEffect.transform.DOScale(200f, 0.25f)
                .OnComplete(() =>
                {
                    cloneParticleEffect.transform.DOScale(0f, 0.25f)
                        .OnComplete(() =>
                        {
                            cloneParticleEffect.SetActive(false);
                        });
                });

            yield return new WaitForSeconds(0.5f);
        }
    }

    #endregion

    #region Bubble Management
    public void DestroyBubble()
    {
        visuals.SetActive(false);

        bubbleCollider.enabled = false;

        DOVirtual.DelayedCall(1f, () =>
        {
            Destroy(gameObject);
        });
    }
    public void DestroyBubbleOnCardContact()
    {
        bubbleCollider.enabled = false;

        if (inGameParticleEffect != null)
        {
            inGameParticleEffect.SetActive(true);
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
