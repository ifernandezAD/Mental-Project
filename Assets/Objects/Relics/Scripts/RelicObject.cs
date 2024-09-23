using UnityEngine;

[CreateAssetMenu(fileName = "RelicObject", menuName = "Scriptable Objects/RelicObject")]
public class RelicObject : ScriptableObject
{   public string relicName;
    public string relicDescription;
    public Sprite relicArt;

    [Header("Normal Relics")]
     public int index;
}
