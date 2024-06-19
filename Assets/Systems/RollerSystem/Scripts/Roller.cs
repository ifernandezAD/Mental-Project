using UnityEngine;

public class Roller : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] bool testAddSlot;
    [SerializeField] bool testRemoveSlot;
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

    void LockSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < lockedSlots.Length)
        {
            lockedSlots[slotIndex] = true;
        }
    }

    void UnlockSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < lockedSlots.Length)
        {
            lockedSlots[slotIndex] = false;
        }
    }

    void ActivateRandomImageInSlots()
    {
        foreach (Transform slot in slots.transform)
        {
            if (slot.gameObject.activeSelf)
            {
                int childCount = slot.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    slot.GetChild(i).gameObject.SetActive(false);
                }


                int randomIndex = Random.Range(0, childCount);
                slot.GetChild(randomIndex).gameObject.SetActive(true);
            }
        }
    }
}
