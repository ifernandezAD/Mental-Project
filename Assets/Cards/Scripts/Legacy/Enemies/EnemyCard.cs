using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCard", menuName = "Scriptable Objects/EnemyCard")]
public class EnemyCard : ScriptableObject
{
    public string cardName;
    public Sprite art;
    public string skill;
    
    public int health;
    public int attack;

    public bool isBoss;

}
