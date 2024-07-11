using System;
using System.Collections.Generic;
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

    private List<ImageCounter> imageCounters = new List<ImageCounter>();

    [Serializable]
    public class ImageCounter
    {
        public ImageType type;
        public int count;
    }

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
        InitializeImageCounters();
    }

    void InitializeImageCounters()
    {
        imageCounters.Clear();
        foreach (ImageType type in Enum.GetValues(typeof(ImageType)))
        {
            imageCounters.Add(new ImageCounter { type = type, count = 0 });
        }
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

        UpdateImageCounters();
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

    void UpdateImageCounters()
    {
        InitializeImageCounters(); // Reset counts

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
                            ImageCounter counter = imageCounters.Find(c => c.type == imageType);
                            if (counter != null)
                            {
                                counter.count++;
                            }
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
        ImageCounter counter = imageCounters.Find(c => c.type == type);
        return counter != null ? counter.count : 0;
    }

    void CalculateRollOutcome()
    {
        foreach (ImageCounter counter in imageCounters)
        {
            Debug.Log($"{counter.type} count is {counter.count}");
        }
    }
}
