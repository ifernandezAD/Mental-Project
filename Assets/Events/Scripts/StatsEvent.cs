using UnityEngine;

public class StatsEvent : GenericEvent
{
    [Header("References")]
    private GameObject characterCard;
    private Health health;
    private Skill skill;

    [Header("Variables")]
    [SerializeField] int healthAmount = 2;
    [SerializeField] int resilienceAmount = 2;
    [SerializeField] int staminaAmount = 3;

    void Start()
    {
        characterCard = GameLogic.instance.mainCharacterCard;
        health = characterCard.GetComponent<Health>();
        skill = characterCard.GetComponent<Skill>();
    }

    public void AddHealthEvent()
    {
        health.AddHealth(healthAmount);
    }

    public void AddResilienceEvent()
    {
        health.AddResilience(resilienceAmount);
    }

    public void AddStaminaEvent()
    {
        skill.IncreaseStamina(staminaAmount);
    }
}
