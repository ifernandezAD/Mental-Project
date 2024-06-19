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

    [Header("Testing Slots")]
    [SerializeField] bool testRoll;

    [Header("Variables")]
    [SerializeField] GameObject slots;
    [SerializeField] int activeSlotCount = 3;
    [SerializeField] bool[] lockedSlots;

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

        if(testLockSlot)
        {
            LockSlot(testLockSlotInt);
            testLockSlot = false;
        }

        if(testUnlockSlot)
        {
            UnlockSlot(testLockSlotInt);
            testUnlockSlot = false;
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
    }


    void AddSlot()
    {
        if (activeSlotCount < slots.transform.childCount)
        {
            slots.transform.GetChild(activeSlotCount).gameObject.SetActive(true);
            activeSlotCount++;
        }
    }

    void RemoveSlot()
    {
        if (activeSlotCount > 3)
        {
            activeSlotCount--;
            slots.transform.GetChild(activeSlotCount).gameObject.SetActive(false);
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

      public bool IsSlotLocked(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < lockedSlots.Length)
        {
            return lockedSlots[slotIndex];
        }
        return false;
    }
    public bool IsImageActiveInSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.transform.childCount)
        {
            Transform slot = slots.transform.GetChild(slotIndex);
            Transform icons = slot.GetChild(0);

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

    void ActivateRandomImageInSlots()
    {
        int slotIndex = 0;
        foreach (Transform slot in slots.transform)
        {
            if (slot.gameObject.activeSelf && !lockedSlots[slotIndex])
            {
                Transform icons = slot.GetChild(0); 
                int childCount = icons.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    icons.GetChild(i).gameObject.SetActive(false);
                }

                int randomIndex = Random.Range(0, childCount);
                icons.GetChild(randomIndex).gameObject.SetActive(true);
            }
            slotIndex++;
        }
    }
}
