using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableObject", menuName = "Scriptable Objects/ConsumableObject")]
public class ConsumableObject : ScriptableObject
{  
    public int index;
    public string consumableName;
    public string consumableDescription;
    public Sprite consumableArt;
}
