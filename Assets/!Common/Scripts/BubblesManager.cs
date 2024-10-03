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
        int shieldCount = roller.GetImageCount(ImageType.Resilience);
        int raysCount = roller.GetImageCount(ImageType.Stamina);
        int poisonCount = roller.GetImageCount(ImageType.Poison);
        int emptyCount = roller.GetImageCount(ImageType.Empty);
        int randomCount = roller.GetImageCount(ImageType.Random);
        int debbufAttackCount = roller.GetImageCount(ImageType.DebuffAttack);
        int multiplyCount = roller.GetImageCount(ImageType.Multiply);
        int cloneCount = roller.GetImageCount(ImageType.Clone);
        int destroyCount = roller.GetImageCount(ImageType.Destroy);
        int splitCount = roller.GetImageCount(ImageType.Split);
        int staminaPoisonCount = roller.GetImageCount(ImageType.StaminaPoison);
        int resiliencePoisonCount = roller.GetImageCount(ImageType.ResiliencePoison);
        int healthCount = roller.GetImageCount(ImageType.Health);


        for (int i = 0; i < swordCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[0], bubbleContainer);
        }

        for (int i = 0; i < shieldCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[1], bubbleContainer);
        }

        for (int i = 0; i < raysCount; i++)
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
        for (int i = 0; i < debbufAttackCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[5], bubbleContainer);
        }
        for (int i = 0; i < multiplyCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[6], bubbleContainer);
        }
        for (int i = 0; i < cloneCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[7], bubbleContainer);
        }
        for (int i = 0; i < destroyCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[8], bubbleContainer);
        }
        for (int i = 0; i < splitCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[9], bubbleContainer);
        }
        for (int i = 0; i < staminaPoisonCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[10], bubbleContainer);
        }
        for (int i = 0; i < resiliencePoisonCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[11], bubbleContainer);
        }
        for (int i = 0; i < healthCount; i++)
        {
            GameObject bubble = Instantiate(bubbles[12], bubbleContainer);
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
            DraggableButton draggableButton = bubble.GetComponent<DraggableButton>();

            if (draggableButton != null)
            {
                int multiplier = draggableButton.bubbleMultiplier;
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

        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }

    public void TransformBubblesToDoubleResilience()
    {
        int bubbleCount = ClearBubblesContainer();

        for (int i = 0; i < bubbleCount; i++)
        {
            GameObject newBubble = Instantiate(bubbles[1], bubbleContainer);
            DraggableButton draggableButton = newBubble.GetComponent<DraggableButton>();

            if (draggableButton != null)
            {
                draggableButton.InitializeMultiplier(2);
            }

        }

        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }
    public void TransformBubblesToDoubleStamina()
    {
        int bubbleCount = ClearBubblesContainer();

        for (int i = 0; i < bubbleCount; i++)
        {
            GameObject newBubble = Instantiate(bubbles[2], bubbleContainer);
            DraggableButton draggableButton = newBubble.GetComponent<DraggableButton>();

            if (draggableButton != null)
            {
                draggableButton.InitializeMultiplier(2);
            }
        }

        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }


    public void AddDoubleSwordBubble()
    {
        GameObject newBubble = Instantiate(bubbles[0], bubbleContainer);

        DraggableButton draggableButton = newBubble.GetComponent<DraggableButton>();

        if (draggableButton != null)
        {
            draggableButton.InitializeMultiplier(2);
        }

        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }

    public void AddDoubleResilienceBubble()
    {
        GameObject newBubble = Instantiate(bubbles[1], bubbleContainer);

        DraggableButton draggableButton = newBubble.GetComponent<DraggableButton>();
        if (draggableButton != null)
        {
            draggableButton.InitializeMultiplier(2);
        }

        EnableLayout();
        DOVirtual.DelayedCall(0.5f, DisableLayout);
    }

    public void AddDoubleStaminaBubble()
    {
        GameObject newBubble = Instantiate(bubbles[2], bubbleContainer);

        DraggableButton draggableButton = newBubble.GetComponent<DraggableButton>();
        if (draggableButton != null)
        {
            draggableButton.InitializeMultiplier(2);
        }
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
