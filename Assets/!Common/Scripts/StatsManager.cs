using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance { get; private set; }

    [Header("References")]
    private GameObject mainCharacterCard;
    private Transform allyContainer;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCharacterCard = GameLogic.instance.mainCharacterCard;
        allyContainer = GameLogic.instance.allyContainer;
    }

    public void ApplyDamageToRandomTarget(int damage)
    {
        List<Health> possibleTargets = GetAllPossibleTargets();

        if (possibleTargets.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, possibleTargets.Count);
            possibleTargets[randomIndex].RemoveHealth(damage);
        }
    }

    public void ApplyDamageToRandomTargetNoResilience(int damage)
    {
        List<Health> possibleTargets = GetAllPossibleTargets();

        if (possibleTargets.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, possibleTargets.Count);
            possibleTargets[randomIndex].RemoveHealthNoResilience(damage);
        }
    }

    public void ReduceStaminaForAll(int amount)
    {
        List<Health> allTargets = GetAllPossibleTargets();

        foreach (Health target in allTargets)
        {
            Skill targetSkill = target.GetComponent<Skill>();
            if (targetSkill != null)
            {
                targetSkill.DecreaseStamina(amount);
            }
        }
    }

    public List<Health> GetAllPossibleTargets()
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
}
