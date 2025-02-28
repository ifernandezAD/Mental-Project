using UnityEngine;
using UnityEngine.UI;

public class SlotButtonHandler : MonoBehaviour
{
    [SerializeField] int slotIndex;
    [SerializeField] GameObject lockedImage;
    [SerializeField] GameObject damageLockedImage;
    private Button button;

    [SerializeField] int lockedDamageDamage = 1;

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
            GameLogic.instance.mainCharacterCard.GetComponent<Health>().RemoveHealth(lockedDamageDamage);

            Slots.instance.UnlockDamageLock(slotIndex);
            ApplyDamageLock(false);
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
