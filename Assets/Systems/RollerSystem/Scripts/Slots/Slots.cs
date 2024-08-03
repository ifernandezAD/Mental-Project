using UnityEngine;

public class Slots : MonoBehaviour
{
    public static Slots instance { get; private set; }

    [Header("Slots Variables")]
    [SerializeField] GameObject slots;
    [SerializeField] int activeSlotCount = 3;
    [SerializeField] public bool[] lockedSlots;

    [Header("Testing Add/Remove Slots")]
    [SerializeField] bool testAddSlot;
    [SerializeField] bool testRemoveSlot;

    [Header("Testing Locking Slots")]
    [SerializeField] int testLockSlotInt;
    [SerializeField] bool testLockSlot;
    [SerializeField] bool testUnlockSlot;

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

    public void AddSlot()
    {
        if (activeSlotCount < slots.transform.childCount)
        {
            slots.transform.GetChild(activeSlotCount).gameObject.SetActive(true);
            ResetSlot(activeSlotCount);
            activeSlotCount++;
        }
    }

    public void RemoveSlot()
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

    public void UnlockAllSlots()
    {
        for (int i = 0; i < lockedSlots.Length; i++)
        {
            lockedSlots[i] = false;


            Transform slot = slots.transform.GetChild(i);
            Transform visuals = slot.GetChild(0);

            if (visuals != null)
            {
                foreach (Transform image in visuals)
                {
                    image.gameObject.SetActive(false);
                }
            }
        }
    }
}
