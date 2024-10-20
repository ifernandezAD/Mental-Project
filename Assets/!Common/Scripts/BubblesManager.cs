using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BubblesManager : MonoBehaviour
{
    public static BubblesManager instance { get; private set; }

    [Header("Bubbles")]
    [SerializeField] GameObject[] bubbles;
    [SerializeField] Transform bubbleContainer;
    [SerializeField] GridLayoutGroup bubblesLayout;
    private Roller roller;
    void Awake()
    {
        instance = this;
        roller= Roller.instance;
    }

    void OnEnable()
    {
        PlayerPhase.onPlayerPhaseEnded += EnableLayout;
    }

    private void InstantiateBubble(int bubbleIndex, int multiplier = 1)
    {
        GameObject newBubble = Instantiate(bubbles[bubbleIndex], bubbleContainer);
        Bubble bubbleClass = newBubble.GetComponent<Bubble>();

        if (bubbleClass != null && multiplier > 1)
        {
            bubbleClass.InitializeMultiplier(multiplier);
        }
    }

    private void InstantiateMultipleBubbles(int count, int bubbleIndex, int multiplier = 1)
    {
        for (int i = 0; i < count; i++)
        {
            InstantiateBubble(bubbleIndex, multiplier);
        }
    }

    public void InstantiateBubbles()
    {
        roller= Roller.instance;
        roller.UpdateImageCount();

        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Sword), 0);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Resilience), 1);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Stamina), 2);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Poison), 3);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Empty), 4);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.DebuffAttack), 5);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Multiply), 6);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Clone), 7);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Destroy), 8);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Split), 9);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.StaminaPoison), 10);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.ResiliencePoison), 11);
        InstantiateMultipleBubbles(roller.GetImageCount(ImageType.Health), 12);
 
        InstantiateRandomBubbles();

        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }

    private void InstantiateRandomBubbles()
    {
        int randomCount = roller.GetImageCount(ImageType.Random);
        for (int i = 0; i < randomCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, 9); //Temporalmente, las 3 ultimas burbujas todavia no estan habilitadas
            Debug.Log("Random index is " + randomIndex);
            InstantiateBubble(randomIndex);
        }
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
            Bubble bubbleClass = bubble.GetComponent<Bubble>();

            if (bubbleClass != null)
            {
                int multiplier = bubbleClass.bubbleMultiplier;
                for (int i = 0; i < multiplier; i++)
                {
                    StatsManager.instance.ApplyDamageToRandomTarget(1);
                }
            }
        }
    }

    #region Skills

    public void TransformBubblesToRandom()
    {
        int bubbleCount = ClearBubblesContainer();
        for (int i = 0; i < bubbleCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, 9); //Temporalmente, las 3 ultimas burbujas todavia no estan habilitadas
            InstantiateBubble(randomIndex);
        }

        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }

    public void TransformEmptyBubblesToDefenseOrAttack()
    {
        foreach (Transform bubble in bubbleContainer)
        {
            if (bubble.CompareTag("Empty"))
            {
                Destroy(bubble.gameObject);
                int randomIndex = UnityEngine.Random.Range(0, 2);
                InstantiateBubble(randomIndex);
            }
        }
        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }

    public void TransformBubblesToDoubleResilience()
    {
        TransformBubblesToType(1, 2);
    }

    public void TransformBubblesToDoubleStamina()
    {
        TransformBubblesToType(2, 2);
    }

    private void TransformBubblesToType(int bubbleIndex, int multiplier)
    {
        int bubbleCount = ClearBubblesContainer();
        InstantiateMultipleBubbles(bubbleCount, bubbleIndex, multiplier);

        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }

    public void AddDoubleSwordBubble()
    {
        AddDoubleBubble(0);
    }

    public void AddDoubleResilienceBubble()
    {
        AddDoubleBubble(1);
    }

    public void AddDoubleStaminaBubble()
    {
        AddDoubleBubble(2);
    }

    private void AddDoubleBubble(int bubbleIndex)
    {
        InstantiateBubble(bubbleIndex, 2);
        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
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

    public void EnableLayout()
    {
        bubblesLayout.enabled = true;
    }

    public void DisableLayout()
    {
        bubblesLayout.enabled = false;
    }

    void OnDisable()
    {
        PlayerPhase.onPlayerPhaseEnded -= EnableLayout;
    }
}
