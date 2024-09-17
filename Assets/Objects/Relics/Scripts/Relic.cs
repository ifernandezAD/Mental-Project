using UnityEngine;

public class Relic : MonoBehaviour
{   
    [SerializeField] public string relicName;
    [SerializeField] public string relicDescription;
    void Start()
    {
        Effect();
    }
    protected virtual void Effect()
    {
        Debug.Log("Base Relic effect triggered.");
    }

}
