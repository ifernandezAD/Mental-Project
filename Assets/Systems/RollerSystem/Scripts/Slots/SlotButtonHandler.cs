using UnityEngine;
using UnityEngine.UI;

public class SlotButtonHandler : MonoBehaviour
{
    [SerializeField] int slotIndex;
    [SerializeField] GameObject lockedImage;
    [SerializeField] GameObject damageLockedImage;
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

        if (Slots.instance.IsDamageLocked(slotIndex))
        {
            Debug.Log("No puedes interactuar con un slot bloqueado por Damage Lock.");
            return;
        }

        if (Slots.instance.IsSlotLocked(slotIndex))
        {
            Slots.instance.UnlockSlot(slotIndex);
            lockedImage.SetActive(false);
        }
        else
        {
            Slots.instance.LockSlot(slotIndex);
            lockedImage.SetActive(true);
        }
    }

    public void ApplyDamageLock(bool state)
    {
        if (damageLockedImage != null)
        {
            damageLockedImage.SetActive(state);
        }
    }
}
