using UnityEngine;
using UnityEngine.UI;


public class SlotButtonHandler : MonoBehaviour
{
    [SerializeField] int slotIndex;
    [SerializeField] GameObject lockedImage;
    private Button button;


    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (!Roller.instance.IsImageActiveInSlot(slotIndex))
        {
            return; 
        }

        if (Roller.instance.IsSlotLocked(slotIndex))
        {
            Roller.instance.UnlockSlot(slotIndex);
            lockedImage.SetActive(false);
        }
        else
        {
            Roller.instance.LockSlot(slotIndex);
            lockedImage.SetActive(true);
        }
    }
}
