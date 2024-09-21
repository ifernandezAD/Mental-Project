using UnityEngine;
using UnityEngine.UI;

public class ConsumableDisplay : MonoBehaviour
{
    [Header("Common References")]
    [SerializeField] public ConsumableObject consumableObject;
    [SerializeField] public Image consumableArt;

    void Start()
    {
        consumableArt.sprite = consumableObject.consumableArt;
    }
}
