using System;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    private CardDisplay cardDisplay;

    [Header("Health")]
    [SerializeField] TextMeshProUGUI livesText;
    private int maxLives = 5;
    private int currentLives = 5;

    [Header("Resilience")]
    private int resilience = 0;
    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
    }

    void Start()
    {
        maxLives = cardDisplay.card.maxHealth;
        currentLives = maxLives;

        livesText.text = currentLives.ToString();
    }

    public void AddHealth(int health)
    {
        currentLives += health;
        livesText.text = currentLives.ToString();
    }

    public void RemoveHealth(int damage)
    {
        int effectiveDamage = Mathf.Max(0, damage - resilience);
        resilience = Mathf.Max(0, resilience - damage);

        currentLives -= effectiveDamage;

        livesText.text = currentLives.ToString();

        if (currentLives <= 0)
        {
            currentLives = 0;

            if (cardDisplay.card.cardType == Card.CardType.Enemy)
            {
                ManageEnemyCardDead();
            }
            if (cardDisplay.card.cardType == Card.CardType.Character)
            {
                ManageCharacterCardDead();
            }
        }
    }

    public void AddResilience(int resilience)
    {
        this.resilience += resilience;
    }

    public void ResetResilience()
    {
        resilience = 0;
    }

    void ManageEnemyCardDead()
    {
        if (cardDisplay.card.isBoss)
        {
            //Haz que el roundmanager pase de acto
        }

        Destroy(gameObject);
    }

    private void ManageCharacterCardDead()
    {
        RoundManager.instance.EndGame();
        Debug.Log("GameOver");
    }
}
