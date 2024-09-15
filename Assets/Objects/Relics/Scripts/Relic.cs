using UnityEngine;

public class Relic : MonoBehaviour
{
    [SerializeField] public string description;
    void Start()
    {
        Effect();
    }
    protected virtual void Effect()
    {
        Debug.Log("Base Relic effect triggered.");
    }

}
