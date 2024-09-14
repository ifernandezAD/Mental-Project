using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] public string description;

    public virtual void Use()
    {
        Debug.Log("Consumable used!");
    }
}
