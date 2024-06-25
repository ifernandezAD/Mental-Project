using UnityEngine;
using TMPro;

public class CharacterMentalHealth : MonoBehaviour
{
    private CharacterCardDisplay characterCardDisplay;

    [Header("References")]

    [SerializeField] Transform enemyCardContainer;

    [Header("MentalHealth")]
    [SerializeField] TextMeshProUGUI mentalHealthText;
    private int maxMentalHealth = 5;
    private int currentMentalHealth = 5;

    [Header("Testing")]
    [SerializeField] private bool testAddLives;
    [SerializeField] private bool testDecreaseLives;


    void OnValidate()
    {
        if (testAddLives)
        {
            ChangeMentalHealth(1);
            testAddLives = false;
        }

        if (testDecreaseLives)
        {
            ChangeMentalHealth(-1);
            testDecreaseLives = false;
        }
    }

    void Awake()
    {
        characterCardDisplay = GetComponent<CharacterCardDisplay>();
    }

    void OnEnable()
    {
        RoundManager.onEnemyPhase += ManageMentalHealthDamageTaken;
    }


    void Start()
    {
        maxMentalHealth = characterCardDisplay.characterCard.maxMentalHealth;
        currentMentalHealth = maxMentalHealth;

        mentalHealthText.text = currentMentalHealth.ToString();
    }

    void ChangeMentalHealth(int lives)
    {
        currentMentalHealth -= lives;

        mentalHealthText.text = currentMentalHealth.ToString();

        if (currentMentalHealth <= 0)
        {
            currentMentalHealth = 0;
            RoundManager.instance.EndGame();
            Debug.Log("GameOver");      
        }
        else
        {
            RoundManager.instance.StartPlayerPhase();
        }
    }
    private void ManageMentalHealthDamageTaken()
    {
        int enemyDamage = enemyCardContainer.GetChild(0).GetComponent<EnemyCardDisplay>().enemyCard.attack;
        int characterResilience = Roller.instance.GetImageCount(ImageType.Heart);

        int netDamage = Mathf.Max(enemyDamage - characterResilience, 0);

        ChangeMentalHealth(enemyDamage);
    }

    void OnDisable()
    {
        RoundManager.onEnemyPhase -= ManageMentalHealthDamageTaken;
    }
}
