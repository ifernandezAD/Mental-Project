using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance { get; private set; }

    [Header("References")]
    private GameObject mainCharacterCard;
    private Transform allyContainer;
    private Transform enemyContainer;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCharacterCard = GameLogic.instance.mainCharacterCard;
        allyContainer = GameLogic.instance.allyContainer;
        enemyContainer = GameLogic.instance.enemyContainer;
    }

    public void ApplyDamageToRandomTarget(int damage)
    {
        List<Health> possibleTargets = GetAllPossibleFriendlyTargets();

        if (possibleTargets.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, possibleTargets.Count);
            possibleTargets[randomIndex].RemoveHealth(damage);
        }
    }

    public void ApplyDamageToRandomTargetNoResilience(int damage)
    {
        List<Health> possibleTargets = GetAllPossibleFriendlyTargets();

        if (possibleTargets.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, possibleTargets.Count);
            possibleTargets[randomIndex].RemoveHealthNoResilience(damage);
        }
    }

    public void ReduceStaminaForAll(int amount)
    {
        List<Health> allTargets = GetAllPossibleFriendlyTargets();

        foreach (Health target in allTargets)
        {
            Skill targetSkill = target.GetComponent<Skill>();
            if (targetSkill != null)
            {
                targetSkill.DecreaseStamina(amount);
            }
        }
    }

    public void HealAllAlliesAndPlayer(int healingAmount)
    {
        List<Health> allTargets = GetAllPossibleFriendlyTargets();

        foreach (Health target in allTargets)
        {
            target.AddHealth(healingAmount);
        }
    }

    public List<Health> GetAllPossibleFriendlyTargets()
    {
        List<Health> possibleTargets = new List<Health>();

        Health mainCharacterHealth = mainCharacterCard.GetComponent<Health>();
        if (mainCharacterHealth != null)
        {
            possibleTargets.Add(mainCharacterHealth);
        }

        for (int i = 0; i < allyContainer.childCount; i++)
        {
            Health allyHealth = allyContainer.GetChild(i).GetComponent<Health>();
            if (allyHealth != null)
            {
                possibleTargets.Add(allyHealth);
            }
        }

        return possibleTargets;
    }

    public List<GameObject> GetAllEnemyTargets()
{
    List<GameObject> enemyCards = new List<GameObject>();

    for (int i = 0; i < enemyContainer.childCount; i++)
    {
        Transform enemySlot = enemyContainer.GetChild(i);
        if (enemySlot.childCount > 0) // Asegura que el slot tiene una carta
        {
            GameObject enemyCard = enemySlot.GetChild(0).gameObject;
            enemyCards.Add(enemyCard);
        }
    }

    return enemyCards;
}

}
