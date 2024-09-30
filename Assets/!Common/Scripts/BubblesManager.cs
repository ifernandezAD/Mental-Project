using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BubblesManager : MonoBehaviour
{
    public static BubblesManager instance { get; private set; }

    [Header("Bubbles")]
    [SerializeField] GameObject[] bubbles;
    [SerializeField] GameObject doubleStaminaBubblePrefab;
    [SerializeField] GameObject doubleResilienceBubblePrefab;
    [SerializeField] GameObject doubleSwordBubblePrefab;
    [SerializeField] Transform bubbleContainer;

    [SerializeField] GridLayoutGroup bubblesLayout;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        PlayerPhase.onPlayerPhaseEnded += EnableLayout;
    }

    public void InstantiateBubbles()
    {
        Roller roller = Roller.instance;
        roller.UpdateImageCount();

        int swordCount = roller.GetImageCount(ImageType.Sword);
        int heartCount = roller.GetImageCount(ImageType.Heart);
        int bookCount = roller.GetImageCount(ImageType.Book);
        int poisonCount = roller.GetImageCount(ImageType.Poison);
        int emptyCount = roller.GetImageCount(ImageType.Empty);
        int randomCount = roller.GetImageCount(ImageType.Random);

        for (int i = 0; i < swordCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[0], bubbleContainer);
        }

        for (int i = 0; i < heartCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[1], bubbleContainer);
        }

        for (int i = 0; i < bookCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[2], bubbleContainer);
        }

        for (int i = 0; i < poisonCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[3], bubbleContainer);
        }

        for (int i = 0; i < emptyCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[4], bubbleContainer);
        }

        for (int i = 0; i < randomCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, bubbles.Length);
            GameObject bubble = Instantiate(bubbles[randomIndex], bubbleContainer);
        }

        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }

    public void DestroyAllBubbles()
    {
        foreach (Transform bubble in bubbleContainer)
        {
            CheckForPoisonBubbles(bubble);

            Destroy(bubble.gameObject);
        }
    }

    private void CheckForPoisonBubbles(Transform bubble)
    {
        if (bubble.CompareTag("Poison"))
        {
            StatsManager.instance.ApplyDamageToRandomTarget(1);
        }
    }

    #region Skills

    public void TransformBubblesToRandom()
    {
        int bubbleCount = ClearBubblesContainer();

        for (int i = 0; i < bubbleCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, bubbles.Length);
            GameObject newBubble = Instantiate(bubbles[randomIndex], bubbleContainer);
        }
    }

    public void TransformEmptyBubblesToDefenseOrAttack()
    {
        foreach (Transform bubble in bubbleContainer)
        {
            if (bubble.CompareTag("Empty"))
            {
                Destroy(bubble.gameObject);

                int randomIndex = UnityEngine.Random.Range(0, 2);
                GameObject newBubble;

                if (randomIndex == 0)
                {
                    newBubble = Instantiate(bubbles[0], bubbleContainer);
                }
                else
                {
                    newBubble = Instantiate(bubbles[1], bubbleContainer);
                }

                newBubble.transform.position = bubble.position;
            }
        }
    }

    public void TransformBubblesToDoubleStamina()
    {
        int bubbleCount = ClearBubblesContainer();

        for (int i = 0; i < bubbleCount; i++)
        {
            GameObject newBubble = Instantiate(doubleStaminaBubblePrefab, bubbleContainer);
        }
    }

    public void TransformBubblesToDoubleResilience()
    {
        int bubbleCount = ClearBubblesContainer();

        for (int i = 0; i < bubbleCount; i++)
        {
            GameObject newBubble = Instantiate(doubleResilienceBubblePrefab, bubbleContainer);
        }
    }

    public void AddDoubleSwordBubble()
    {
        GameObject newBubble = Instantiate(doubleSwordBubblePrefab, bubbleContainer);
    }

    public void AddDoubleStaminaBubble()
    {
        GameObject newBubble = Instantiate(doubleStaminaBubblePrefab, bubbleContainer);
    }

    public void AddDoubleResilienceBubble()
    {
        GameObject newBubble = Instantiate(doubleResilienceBubblePrefab, bubbleContainer);
    }

    private int ClearBubblesContainer()
    {
        int bubbleCount = bubbleContainer.childCount;

        foreach (Transform bubble in bubbleContainer)
        {
            Destroy(bubble.gameObject);
        }

        return bubbleCount;
    }

    #endregion

    void EnableLayout()
    {
        bubblesLayout.enabled = true;
    }

    void DisableLayout()
    {
        bubblesLayout.enabled = false;
    }

    void OnDisable()
    {
        PlayerPhase.onPlayerPhaseEnded -= EnableLayout;
    }
}
