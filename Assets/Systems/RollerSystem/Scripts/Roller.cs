using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Roller : MonoBehaviour
{
    public static Roller instance { get; private set; }

    [Header("Testing Slots")]
    [SerializeField] bool testRoll;

    [Header("Testing Roll Outcome")]
    [SerializeField] bool testOutcome;

    [Header("References")]
    [SerializeField] GameObject slots;

    private Dictionary<ImageType, int> imageCount = new Dictionary<ImageType, int>();

    void OnValidate()
    {
        if (testRoll)
        {
            ActivateRandomImageInSlots();
            testRoll = false;
        }

        if (testOutcome)
        {
            CalculateRollOutcome();
            testOutcome = false;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitializeImageCount();     
    }
    
    public bool IsImageActiveInSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.transform.childCount)
        {
            Transform slot = slots.transform.GetChild(slotIndex);
            Transform icons = slot.GetChild(1);

            foreach (Transform child in icons)
            {
                if (child.gameObject.activeSelf)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ActivateRandomImageInSlots()
    {
        int currentEnergy = Energy.instance.GetCurrentEnergy();

        if (currentEnergy <= 0)
        {
            return;
        }

        if (currentEnergy == 1)
        {
            Energy.instance.TriggerOnOutOfEnergy();
        }

        int slotIndex = 0;
        foreach (Transform slot in slots.transform)
        {
            if (slot.gameObject.activeSelf && !Slots.instance.lockedSlots[slotIndex])
            {
                Transform icons = slot.GetChild(1);
                int childCount = icons.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    icons.GetChild(i).gameObject.SetActive(false);
                }

                int randomIndex = UnityEngine.Random.Range(0, childCount);
                icons.GetChild(randomIndex).gameObject.SetActive(true);
            }
            slotIndex++;
        }

        UpdateImageCount();
        Energy.instance.RemoveEnergy();

        CalculateRollOutcome(); //Testing
    }

    public void DisableAllSlotImages()
    {
        foreach (Transform slot in slots.transform)
        {
            Transform icons = slot.GetChild(1);
            foreach (Transform icon in icons)
            {
                icon.gameObject.SetActive(false);
            }
        }
    }

    void InitializeImageCount()
    {
        imageCount.Clear();
        imageCount.Add(ImageType.Sword, 0);
        imageCount.Add(ImageType.Heart, 0);
        imageCount.Add(ImageType.Book, 0);
        imageCount.Add(ImageType.Poison,0);
    }

    void UpdateImageCount()
    {
        InitializeImageCount(); // Reset counts

        foreach (Transform slot in slots.transform)
        {
            if (slot.gameObject.activeSelf)
            {
                Transform icons = slot.GetChild(1);
                foreach (Transform icon in icons)
                {
                    if (icon.gameObject.activeSelf)
                    {
                        ImageType imageType = GetImageType(icon.gameObject);
                        if (imageType != ImageType.None)
                        {
                            imageCount[imageType]++;
                        }
                    }
                }
            }
        }
    }

    ImageType GetImageType(GameObject imageObject)
    {
        if (imageObject.CompareTag("Sword"))
        {
            return ImageType.Sword;
        }
        else if (imageObject.CompareTag("Heart"))
        {
            return ImageType.Heart;
        }
        else if (imageObject.CompareTag("Book"))
        {
            return ImageType.Book;
        }

        else if (imageObject.CompareTag("Poison"))
        {
            return ImageType.Poison;
        }

        return ImageType.None; 
    }

    public int GetImageCount(ImageType type)
    {
        if (imageCount.ContainsKey(type))
        {
            return imageCount[type];
        }
        return 0;
    }

    void CalculateRollOutcome()
    {
        int swordCount = GetImageCount(ImageType.Sword);
        int heartCount = GetImageCount(ImageType.Heart);
        int bookCount = GetImageCount(ImageType.Book);
        int poisonCount=GetImageCount(ImageType.Poison);

        Debug.Log($"Swords count is {swordCount}");
        Debug.Log($"Hearts count is {heartCount}");
        Debug.Log($"Books count is {bookCount}");
        Debug.Log($"Poison count is {poisonCount}");
    }

}
