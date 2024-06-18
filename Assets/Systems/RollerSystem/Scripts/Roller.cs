using UnityEngine;

public class Roller : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] bool testAddSlot;
    [SerializeField] bool testRemoveSlot;

    [Header("Variables")]
    [SerializeField] GameObject slots; 
    [SerializeField] int activeSlotCount = 3; 

void OnValidate()
{
    if(testAddSlot)
    {
        AddSlot();
        testAddSlot = false;
    }

    if(testRemoveSlot)
    {
        RemoveSlot();
        testRemoveSlot = false;
    }
}

 void Start()
    {
        int slotIndex = 0;
        foreach (Transform slot in slots.transform)
        {
            if (slotIndex < activeSlotCount)
            {
                slot.gameObject.SetActive(true);
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
            slotIndex++;
        }
    }

    public void AddSlot()
    {
        if (activeSlotCount < slots.transform.childCount)
        {
            slots.transform.GetChild(activeSlotCount).gameObject.SetActive(true);
            activeSlotCount++;
        }
    }

    public void RemoveSlot()
    {
        if (activeSlotCount > 0)
        {
            activeSlotCount--;
            slots.transform.GetChild(activeSlotCount).gameObject.SetActive(false);
        }
    }
}
