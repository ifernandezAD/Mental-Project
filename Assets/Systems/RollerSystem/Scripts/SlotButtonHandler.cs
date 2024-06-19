using UnityEngine;
using UnityEngine.UI;


public class SlotButtonHandler : MonoBehaviour
{
    [SerializeField] int slotIndex;
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
            return; // Prevent locking if no image is active
        }

        if (Roller.instance.IsSlotLocked(slotIndex))
        {
            Roller.instance.UnlockSlot(slotIndex);
        }
        else
        {
            Roller.instance.LockSlot(slotIndex);
        }
    }
}
