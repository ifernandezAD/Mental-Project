using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CharacterSelector : MonoBehaviour
{
    [SerializeField] CharacterCard[] charactersArray;
    public CharacterCard selectedCharacter;
}
