using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    public enum CardType{Character,Ally,Enemy}
    public CardType cardType;

    [Header("Common Variables")]
    public string cardName;
    public Sprite art;
    public int maxHealth;
    public string skill;    
        
    [Header("Enemy Variables")]
     public int attack;   
     public bool isBoss;
}
