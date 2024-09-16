using System;
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

    [Header("Relics")]
    private bool canSurviveLethalHit = false;
    private bool hasUsedLethalSurvival = false;
    private bool gainResilienceOnLostHealth = false;
    private bool canReviveWithHalfLife = false;
    private bool hasRevived = false;

    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
    }

    void Start()
    {
        maxLives = cardDisplay.card.maxHealth;
        currentLives = maxLives;
        UpdateLivesText();
    }

    public void AddHealth(int health)
    {
        currentLives += health;
        if (currentLives > maxLives) currentLives = maxLives;
        UpdateLivesText();
    }

    public void RemoveHealth(int damage)
    {
        int effectiveDamage = HandleDamageReduction(damage);
        ApplyDamage(effectiveDamage);
        CheckForRelicEffects(effectiveDamage);
        HandleCardDeath();
    }

    public void RemoveHealthNoResilience(int damage)
    {
        ApplyDamage(damage);
        HandleCardDeath();
    }

    private void ApplyDamage(int damage)
    {
        currentLives -= damage;
        UpdateLivesText();
    }

    private int HandleDamageReduction(int damage)
    {
        if (cardDisplay.card.cardType == Card.CardType.Enemy)
        {
            return damage;
        }

        int damageAfterResilience = Mathf.Max(0, damage - resilience);
        resilience = Mathf.Max(0, resilience - damage);
        UpdateResilienceText();
        
        return damageAfterResilience;
    }

    private void CheckForRelicEffects(int effectiveDamage)
    {
        if (effectiveDamage > 0)
        {
            onDirectDamage?.Invoke(this);
            
            if (gainResilienceOnLostHealth)
            {
                AddResilience(1); 
            }
        }

        if (currentLives <= 0)
        {
            HandleLethalSurvival();
            HandleReviveWithHalfLife();
        }
    }

    private void HandleLethalSurvival()
    {
        if (canSurviveLethalHit && !hasUsedLethalSurvival)
        {
            currentLives = 1; 
            hasUsedLethalSurvival = true;
            UpdateLivesText();
        }
    }

    private void HandleReviveWithHalfLife()
    {
        if (canReviveWithHalfLife && !hasRevived)
        {
            currentLives = Mathf.CeilToInt(maxLives / 2f); 
            hasRevived = true;
            UpdateLivesText();
        }
    }

    private void HandleCardDeath()
    {
        if (currentLives <= 0)
        {
            currentLives = 0;
            UpdateLivesText();

            if (cardDisplay.card.cardType == Card.CardType.Enemy)
            {
                ManageEnemyCardDeath();
            }
            else if (cardDisplay.card.cardType == Card.CardType.Character)
            {
                ManageCharacterCardDeath();
            }
            else if (cardDisplay.card.cardType == Card.CardType.Ally)
            {
                ManageAllyCardDeath();
            }
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = currentLives.ToString();
    }

    private void UpdateResilienceText()
    {
        resilienceText.text = resilience.ToString();
    }

    private void ManageEnemyCardDeath()
    {
        if (cardDisplay.card.isBoss)
        {
            onBossDefeated?.Invoke();
        }
        Destroy(gameObject);
    }

    private void ManageCharacterCardDeath()
    {
        RoundManager.instance.EndGame();
        Debug.Log("GameOver");
    }

    private void ManageAllyCardDeath()
    {
        Slots.instance.RemoveSlot();
        Destroy(gameObject);
    }

    public void AddResilience(int resilience)
    {
        this.resilience += resilience;
        UpdateResilienceText();
    }

    public void ResetResilience()
    {
        resilience = 0;
        UpdateResilienceText();
    }

    #region Relics

    public void EnableLethalSurvival()
    {
        canSurviveLethalHit = true;
    }

    public void EnableResilienceOnLostHealth()
    {
        gainResilienceOnLostHealth = true;
    }

    public void EnableReviveWithHalfLife()
    {
        canReviveWithHalfLife = true;
    }

    #endregion
}
