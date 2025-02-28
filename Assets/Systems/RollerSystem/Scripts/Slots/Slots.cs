using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public static Slots instance { get; private set; }

    [Header("Slots Variables")]
    [SerializeField] GameObject slots;
    [SerializeField] int activeSlotCount = 3;
    [SerializeField] public bool[] lockedSlots;
    [SerializeField] public bool[] damageLockedSlots; // Nueva variable para Damage Lock

    [Header("Testing Add/Remove Slots")]
    [SerializeField] bool testAddSlot;
    [SerializeField] bool testRemoveSlot;

    [Header("Testing Locking Slots")]
    [SerializeField] int testLockSlotInt;
    [SerializeField] bool testLockSlot;
    [SerializeField] bool testUnlockSlot;

    [Header("Testing Damage Lock")]
    [SerializeField] bool testApplyDamageLock;
    [SerializeField] bool testClearDamageLocks;

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

        if (testApplyDamageLock)
        {
            ApplyDamageLockToRandomSlots();
            testApplyDamageLock = false;
        }

        if (testClearDamageLocks)
        {
            ClearDamageLocks();
            testClearDamageLocks = false;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        int totalSlots = slots.transform.childCount;

        if (lockedSlots == null || lockedSlots.Length != totalSlots)
        {
            lockedSlots = new bool[totalSlots];
        }

        if (damageLockedSlots == null || damageLockedSlots.Length != totalSlots)
        {
            damageLockedSlots = new bool[totalSlots];
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
            if (damageLockedSlots[slotIndex])
            {
                Debug.LogWarning("¡Intentaste bloquear un slot con Damage Lock!");
                return;
            }
            lockedSlots[slotIndex] = true;
        }
    }

    public void UnlockSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < lockedSlots.Length && !damageLockedSlots[slotIndex])
        {
            lockedSlots[slotIndex] = false;
        }
    }

    public void UnlockDamageLock(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < damageLockedSlots.Length)
        {
            damageLockedSlots[slotIndex] = false;
        }
    }

    public void ApplyDamageLockToRandomSlots()
    {
        List<int> availableSlots = new List<int>();

        for (int i = 0; i < activeSlotCount; i++)
        {
            if (!damageLockedSlots[i])
            {
                availableSlots.Add(i);
            }
        }

        if (availableSlots.Count < 2)
        {
            Debug.LogWarning("No hay suficientes slots disponibles para aplicar Damage Lock.");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableSlots.Count);
            int selectedSlot = availableSlots[randomIndex];

            damageLockedSlots[selectedSlot] = true;
            availableSlots.RemoveAt(randomIndex);

            SlotButtonHandler slotHandler = slots.transform.GetChild(selectedSlot).GetComponent<SlotButtonHandler>();
            if (slotHandler != null)
            {
                slotHandler.ApplyDamageLock(true);
            }

            Debug.Log($"Slot {selectedSlot} ha sido bloqueado con Damage Lock.");
        }
    }

    public void ClearDamageLocks()
    {
        for (int i = 0; i < damageLockedSlots.Length; i++)
        {
            if (damageLockedSlots[i])
            {
                damageLockedSlots[i] = false;
                lockedSlots[i] = false;

                SlotButtonHandler slotHandler = slots.transform.GetChild(i).GetComponent<SlotButtonHandler>();
                if (slotHandler != null)
                {
                    slotHandler.ApplyDamageLock(false);
                }
            }
        }

        Debug.Log("Se han eliminado todos los Damage Locks.");
    }

    public bool IsSlotLocked(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < lockedSlots.Length)
        {
            return lockedSlots[slotIndex];
        }
        return false;
    }

    public bool IsDamageLocked(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < damageLockedSlots.Length)
        {
            return damageLockedSlots[slotIndex];
        }
        return false;
    }

    public void UnlockAllSlots()
    {
        for (int i = 0; i < lockedSlots.Length; i++)
        {
            lockedSlots[i] = false;
            damageLockedSlots[i] = false;

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

            if (damageLockedSlots != null && slotIndex < damageLockedSlots.Length)
            {
                damageLockedSlots[slotIndex] = false;
            }
        }
    }
}
