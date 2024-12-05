using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class CombatBehaviour : MonoBehaviour
{
    protected CardDisplay cardDisplay;
    protected Health health;
    [SerializeField] protected TextMeshProUGUI attackText;
    protected int enemyDamage;

    [Header("Attack Animation")]
    [SerializeField] private float attackMoveDistance = 0.5f;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private float returnDuration = 0.1f;

    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
        health = GetComponent<Health>();
        enemyDamage = cardDisplay.card.attack;
    }
    public virtual void Attack()
    {
        AnimateAttack(() =>
        {
            StatsManager.instance.ApplyDamageToRandomTarget(enemyDamage);
        });
    }

    public virtual void Defense(int damage)
    {
        health.RemoveHealth(damage);
    }

    public void DebuffAttack(int multiplier)
    {
        enemyDamage -= multiplier;

        if (enemyDamage <= 0)
        {
            enemyDamage = 0;
        }

        attackText.text = enemyDamage.ToString();
    }

    protected void BuffAttack()
    {
        enemyDamage++;
        attackText.text = enemyDamage.ToString();
    }

    private void AnimateAttack(Action onComplete)
    {
        Vector3 originalPosition = transform.position;

        // Crear secuencia de animación
        Sequence attackSequence = DOTween.Sequence();

        // Mover hacia adelante
        attackSequence.Append(transform.DOMove(originalPosition + Vector3.up * attackMoveDistance, attackDuration)
            .SetEase(Ease.OutQuad));

        // Retroceder a la posición original
        attackSequence.Append(transform.DOMove(originalPosition, returnDuration)
            .SetEase(Ease.InQuad));

        // Callback al completar la animación
        attackSequence.OnComplete(() => onComplete?.Invoke());
    }
}
