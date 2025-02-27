using System;
using UnityEngine;
using UnityEngine.UI;

public class BubbleDetector : MonoBehaviour
{
    private Card card;
    private Health health;
    private Skill skill;
    private CombatBehaviour combatBehaviour;

    [Header("Relics")]
    public static Action onSwordUsed;

    [Header("Visuals")]
    [SerializeField] private Outline outlineMain;
    [SerializeField] private Outline outlineCutout;

    void Awake()
    {
        card = GetComponent<CardDisplay>().card;
        health = GetComponent<Health>();
        skill = GetComponent<Skill>();

        if (card.cardType == Card.CardType.Enemy)
        {
            combatBehaviour = GetComponent<CombatBehaviour>();
        }
    }

    public void CheckButtonType(DraggableButton button, int multiplier)
    {
        if (button.gameObject.tag == "Attack")
        {
            if (card.cardType == Card.CardType.Enemy)
            {
                combatBehaviour.Defense(multiplier);
                onSwordUsed?.Invoke();

                button.GetComponent<Bubble>().DestroyBubbleOnCardContact();
            }
        }


        if (button.gameObject.tag == "Resilience")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                health.AddResilience(multiplier);
                button.GetComponent<Bubble>().DestroyBubbleOnCardContact();
            }
        }


        if (button.gameObject.tag == "Stamina")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                skill.IncreaseStamina(multiplier);
                button.GetComponent<Bubble>().DestroyBubbleOnCardContact();
            }
        }

        if (button.gameObject.tag == "Health")
        {
            if ((card.cardType == Card.CardType.Character) || card.cardType == Card.CardType.Ally)
            {
                health.AddHealth(multiplier);
                button.GetComponent<Bubble>().DestroyBubbleOnCardContact();
            }
        }

        if (button.gameObject.tag == "DebuffAttack")
        {
            if (card.cardType == Card.CardType.Enemy)
            {
                combatBehaviour.DebuffAttack(multiplier);
                button.GetComponent<Bubble>().DestroyBubbleOnCardContact();
            }
        }
    }

    public bool ShouldEnableOutline(string bubbleTag)
    {
        if ((bubbleTag == "Attack" || bubbleTag == "DebuffAttack") && card.cardType == Card.CardType.Enemy)
        {
            return true;
        }

        if ((bubbleTag == "Resilience" || bubbleTag == "Stamina" || bubbleTag == "Health") &&
            (card.cardType == Card.CardType.Character || card.cardType == Card.CardType.Ally))
        {
            return true;
        }

        return false;
    }


    public void EnableOutline()
    {
        outlineMain.enabled=true;
        outlineCutout.enabled=true;
    }

    public void DisableOutline()
    {
        outlineMain.enabled=false;
        outlineCutout.enabled=false;
    }

}