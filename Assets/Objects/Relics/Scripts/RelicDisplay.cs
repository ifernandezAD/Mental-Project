using UnityEngine;
using UnityEngine.UI;

public class RelicDisplay : MonoBehaviour
{
    [Header("Common References")]
    [SerializeField] public RelicObject relicObject;
    [SerializeField] public Image relicArt;

    void Start()
    {
        relicArt.sprite = relicObject.relicArt;
    }
}
