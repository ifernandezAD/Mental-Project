using UnityEngine;

public class BubblesManager : MonoBehaviour
{
    public static BubblesManager instance { get; private set; }

    [Header("Bubbles")]
    [SerializeField] GameObject[] bubbles;
    [SerializeField] Transform bubbleContainer;
    void Awake()
    {
        instance = this;
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
    }

    public void DestroyAllBubbles()
    {
        foreach (Transform bubble in bubbleContainer)
        {
            if (bubble.CompareTag("Poison"))
            {
                StatsManager.instance.ApplyDamageToRandomTarget(1);
            }

            Destroy(bubble.gameObject);
        }
    }

    #region Skills
    public void TransformBubblesToRandom()
    {
        int bubbleCount = bubbleContainer.childCount;

        foreach (Transform bubble in bubbleContainer)
        {
            Destroy(bubble.gameObject);
        }

        for (int i = 0; i < bubbleCount - 1; i++)
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
    #endregion

}
