using UnityEngine;

public class SnakeCombat : CombatBehaviour
{
    [SerializeField] GameObject poisonPrefab;

    private void OnEnable()
    {
        Health.onDirectDamage += HandleDirectDamage;
    }


    private void HandleDirectDamage(Health target)
    {
        if (target.gameObject.GetComponent<CardDisplay>().card.cardType == Card.CardType.Character)
        {
            Roller.instance.AddImagePrefab(ImageType.Poison,poisonPrefab); 
        }
    }

    private void OnDisable()
    {
        Health.onDirectDamage -= HandleDirectDamage;
    }
}
