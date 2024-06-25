using System;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyCardDisplay enemyCardDisplay;

    [Header("Health")]
    [SerializeField] TextMeshProUGUI livesText;
     private int maxLives = 5;
     private int currentLives = 5;

    [Header("Testing")]
    [SerializeField] private bool testAddLives;
    [SerializeField] private bool testDecreaseLives;


    void OnValidate()
    {
        if(testAddLives)
        {
            ChangeLives(1);
            testAddLives=false;
        }

        if(testDecreaseLives)
        {
            ChangeLives(-1);
            testDecreaseLives=false;
        }  
    }

    void Awake()
    {
        enemyCardDisplay=GetComponent<EnemyCardDisplay>();
    }

    void OnEnable()
    {
        RoundManager.onDamageResolution += ManageEnemyCardDamageTaken;
    }


    void Start()
    {
        maxLives = enemyCardDisplay.enemyCard.health;
        currentLives = maxLives;

        livesText.text = currentLives.ToString();
    }

    void ChangeLives(int lives)
    {
        currentLives -= lives;

        livesText.text = currentLives.ToString();

        if (currentLives <= 0)
        {
            currentLives=0;
            ManageEnemyCardDead();       
        }
        else
        {
            RoundManager.instance.StartEnemyActionPhase();
        }
    }
    private void ManageEnemyCardDamageTaken()
    {
        int damageTaken = Roller.instance.GetImageCount(ImageType.Sword);
        ChangeLives(damageTaken);
    }

    void ManageEnemyCardDead()
    {
        if(!enemyCardDisplay.enemyCard.isBoss)
        {
            RoundManager.instance.StartNextRound();
        }
        else
        {
            RoundManager.instance.StartNextAct();
        }

        Destroy(gameObject);
    }

       void OnDisable()
    {
        RoundManager.onDamageResolution -= ManageEnemyCardDamageTaken;
    }
}
