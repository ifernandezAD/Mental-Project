using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    public enum CardType{Character,Ally,Enemy}
    public CardType cardType;

    [Header("Display")]
    public string cardName;
    public Sprite art;

    [Header("Stats")]
    public int maxHealth;

    [Header("Skills")]
    public int staminaCost;
    public string skillDescription;
        
    [Header("Enemy Variables")]
     public int attack;   
     public bool isBoss;

     


}
