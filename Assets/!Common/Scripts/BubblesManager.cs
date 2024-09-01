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
    public  void InstantiateBubbles()
    {
        Roller roller = Roller.instance;
        roller.UpdateImageCount();

        int swordCount = roller.GetImageCount(ImageType.Sword);
        int heartCount = roller.GetImageCount(ImageType.Heart);
        int bookCount = roller.GetImageCount(ImageType.Book);
        int poisonCount = roller.GetImageCount(ImageType.Poison);
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
                AttackManager.instance.ApplyDamageToRandomTarget(1);
            }

            Destroy(bubble.gameObject);
        }
    }
}
