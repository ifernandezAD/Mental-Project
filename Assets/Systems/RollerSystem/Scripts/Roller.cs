using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Roller : MonoBehaviour
{
    public static Roller instance { get; private set; }

    [Header("Testing Add/Remove Slots")]
    [SerializeField] bool testAddSlot;
    [SerializeField] bool testRemoveSlot;

    [Header("Testing Locking Slots")]
    [SerializeField] int testLockSlotInt;
    [SerializeField] bool testLockSlot;
    [SerializeField] bool testUnlockSlot;

    [Header("Testing Energy")]
    [SerializeField] bool testAddEnergy;
    [SerializeField] bool testRemoveEnergy;
    [SerializeField] bool testResetEnergy;


    [Header("Testing Slots")]
    [SerializeField] bool testRoll;

    [Header("Testing Roll Outcome")]
    [SerializeField] bool testOutcome;

    [Header("Slots Variables")]
    [SerializeField] GameObject slots;
    [SerializeField] int activeSlotCount = 3;
    [SerializeField] bool[] lockedSlots;

    [Header("Energy Variables")]
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] int maxEnergy = 3;
    [SerializeField] int currentEnergy = 3;
    public static Action onOutOfEnergy;
    public static Action onResetEnergy;


    private Dictionary<ImageType, int> imageCount = new Dictionary<ImageType, int>();

    void OnValidate()
    {
        if (testAddSlot)
        {
            AddSlot();
            testAddSlot = false;
        }

        if (testRemoveSlot)
        {
            RemoveSlot();
            testRemoveSlot = false;
        }

        if (testRoll)
        {
            ActivateRandomImageInSlots();
            testRoll = false;
        }

        if (testLockSlot)
        {
            LockSlot(testLockSlotInt);
            testLockSlot = false;
        }

        if (testUnlockSlot)
        {
            UnlockSlot(testLockSlotInt);
            testUnlockSlot = false;
        }

        if (testOutcome)
        {
            CalculateRollOutcome();
            testOutcome = false;
        }

        if (testAddEnergy)
        {
            ChangeMaxEnergy(1);
            testAddEnergy = false;
        }

        if (testRemoveEnergy)
        {
            ChangeMaxEnergy(-1);
            testRemoveEnergy = false;
        }

        if (testResetEnergy)
        {
            ResetEnergy();
            testResetEnergy = false;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (lockedSlots == null || lockedSlots.Length != slots.transform.childCount)
        {
            lockedSlots = new bool[slots.transform.childCount];
        }

        InitializeImageCount();

        energyText.text ="Energy: " + maxEnergy;
    }


    #region Slots

    void AddSlot()
    {
        if (activeSlotCount < slots.transform.childCount)
        {
            slots.transform.GetChild(activeSlotCount).gameObject.SetActive(true);
            ResetSlot(activeSlotCount);
            activeSlotCount++;
        }
    }

    void RemoveSlot()
    {
        if (activeSlotCount > 3)
        {
            activeSlotCount--;
            slots.transform.GetChild(activeSlotCount).gameObject.SetActive(false);
            ResetSlot(activeSlotCount);
        }
    }

    public void LockSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < lockedSlots.Length)
        {
            lockedSlots[slotIndex] = true;
        }
    }

    public void UnlockSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < lockedSlots.Length)
        {
            lockedSlots[slotIndex] = false;
        }
    }

    void ResetSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.transform.childCount)
        {
            Transform slot = slots.transform.GetChild(slotIndex);
            Transform icons = slot.GetChild(1);
            int childCount = icons.childCount;

            for (int i = 0; i < childCount; i++)
            {
                icons.GetChild(i).gameObject.SetActive(false);
            }

            if (lockedSlots != null && slotIndex < lockedSlots.Length)
            {
                lockedSlots[slotIndex] = false;
            }
        }
    }

    public bool IsSlotLocked(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < lockedSlots.Length)
        {
            return lockedSlots[slotIndex];
        }
        return false;
    }

    #endregion

    #region RollImages

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
        if (currentEnergy <= 0)
        {
            return;
        }

        if(currentEnergy == 1)
        {
            onOutOfEnergy?.Invoke();
        }

        int slotIndex = 0;
        foreach (Transform slot in slots.transform)
        {
            if (slot.gameObject.activeSelf && !lockedSlots[slotIndex])
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
        RemoveEnergy();
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

        Debug.Log($"Swords count is {swordCount}");
        Debug.Log($"Hearts count is {heartCount}");
        Debug.Log($"Books count is {bookCount}");
    }

    #endregion


    #region Energy
    public void ChangeMaxEnergy(int energy)
    {
        maxEnergy += energy;
    }

    void RemoveEnergy()
    {
        currentEnergy--;
        energyText.text ="Energy: " + currentEnergy;
    }
    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        energyText.text ="Energy: " + maxEnergy;

        onResetEnergy?.Invoke();
    }

    #endregion

}
