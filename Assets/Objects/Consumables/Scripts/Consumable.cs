using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] public string consumableName;
    [SerializeField] public string consumableDescription;

    public virtual void Use(){}
}
