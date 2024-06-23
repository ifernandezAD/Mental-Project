using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class CharacterCard : ScriptableObject
{
    public string characterName;
    public Sprite art;
    public int maxMentalHealth;
    public string skill;
}
