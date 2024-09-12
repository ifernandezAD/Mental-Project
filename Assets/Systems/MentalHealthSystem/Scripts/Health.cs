using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action onBossDefeated;
    public static Action<Health> onDirectDamage;
    private CardDisplay cardDisplay;

    [Header("Health")]
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI resilienceText;
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

        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }

        livesText.text = currentLives.ToString();
    }

    public void RemoveHealth(int damage)
    {
        int effectiveDamage;

        if (cardDisplay.card.cardType == Card.CardType.Enemy)
        {
            effectiveDamage = damage;
        }
        else
        {
            int damageAfterResilience = Mathf.Max(0, damage - resilience);
            effectiveDamage = damageAfterResilience;

            resilience = Mathf.Max(0, resilience - damage);
            resilienceText.text = resilience.ToString();

            if (damageAfterResilience > 0)
            {
                onDirectDamage?.Invoke(this); 
            }
        }

        currentLives -= effectiveDamage;
        livesText.text = currentLives.ToString();

        HandleCardDeath();
    }
    public void RemoveHealthNoResilience(int damage)
    {
        int effectiveDamage = damage;
        currentLives -= effectiveDamage;
        livesText.text = currentLives.ToString();

        HandleCardDeath();
    }

    private void HandleCardDeath()
    {
        if (currentLives <= 0)
        {
            currentLives = 0;

            if (cardDisplay.card.cardType == Card.CardType.Enemy)
            {
                ManageEnemyCardDead();
            }
            else if (cardDisplay.card.cardType == Card.CardType.Character)
            {
                ManageCharacterCardDead();
            }
            else if (cardDisplay.card.cardType == Card.CardType.Ally)
            {
                ManageAllyCardDead();
            }
        }
    }

    void ManageEnemyCardDead()
    {
        if (cardDisplay.card.isBoss)
        {
            onBossDefeated?.Invoke();
        }

        Destroy(gameObject);
    }

    private void ManageCharacterCardDead()
    {
        RoundManager.instance.EndGame();
        Debug.Log("GameOver");
    }

    private void ManageAllyCardDead()
    {
        Slots.instance.RemoveSlot();
        Destroy(gameObject);
    }

    public void AddResilience(int resilience)
    {
        this.resilience += resilience;
        resilienceText.text = this.resilience.ToString();
    }

    public void ResetResilience()
    {
        resilience = 0;
        resilienceText.text = resilience.ToString();
    }

}
