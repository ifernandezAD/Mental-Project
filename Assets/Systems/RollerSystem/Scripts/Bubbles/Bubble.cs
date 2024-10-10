using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    private RectTransform rectTransform;

    public int bubbleMultiplier { get; private set; } = 1;
    private bool hasCombined = false;

    [Header("Multiplier UI")]
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] Image multiplierBackground;

    [Header("Growth Factor")]
    [SerializeField] private float growthFactor = 0.2f;

    [Header("Fusion Control")]
    [SerializeField] private float fusionCooldown = 0.5f;
    private bool fusionReady = true;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        multiplierBackground.gameObject.SetActive(false);
        multiplierText.gameObject.SetActive(false);
    }

    public void HandleCollision(Collider2D other)
    {
        if (other.TryGetComponent<Bubble>(out Bubble otherBubble))
        {
            if (other.CompareTag(gameObject.tag))
            {
                if (fusionReady && !otherBubble.hasCombined)
                {
                    StartCoroutine(CombineBubbles(otherBubble));
                }
            }
        }
    }

    private IEnumerator CombineBubbles(Bubble otherBubble)
    {
        otherBubble.hasCombined = true;
        fusionReady = false;

        bubbleMultiplier += otherBubble.bubbleMultiplier;
        rectTransform.localScale = Vector3.one * (1 + growthFactor * bubbleMultiplier);

        UpdateMultiplierDisplay();
        Destroy(otherBubble.gameObject);

        yield return new WaitForSeconds(fusionCooldown);
        fusionReady = true;
    }

    public void InitializeMultiplier(int multiplier)
    {
        bubbleMultiplier = multiplier;
        rectTransform.localScale = Vector3.one * (1 + growthFactor * bubbleMultiplier);
        UpdateMultiplierDisplay();
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
}
