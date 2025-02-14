using UnityEngine;

public class StatsEvent : GenericEvent
{
    [Header("References")]
    private GameObject characterCard;
    private Health health;
    private Skill skill;

    [Header("Variables")]
    [SerializeField] int defaultHealthAmount = 2;
    [SerializeField] int defaultResilienceAmount = 2;
    [SerializeField] int defaultStaminaAmount = 3;

    void Start()
    {
        characterCard = GameLogic.instance.mainCharacterCard;
        health = characterCard.GetComponent<Health>();
        skill = characterCard.GetComponent<Skill>();
    }

    public void AddHealthEvent()
    {
        health.AddHealth(GetAmountForStat(defaultHealthAmount));
    }

    public void AddResilienceEvent()
    {
        health.AddResilience(GetAmountForStat(defaultResilienceAmount));
    }

    public void AddStaminaEvent()
    {
        skill.IncreaseStamina(GetAmountForStat(defaultStaminaAmount));
    }

    private int GetAmountForStat(int defaultAmount)
    {
        if (IsFlashback)
        {
            if (IsGoodFlashback)
            {
                return defaultAmount + 1; 
            }
            else
            {
                return Mathf.Max(1, defaultAmount - 1); 
            }
        }
        else
        {
            return defaultAmount; 
        }
    }
}
