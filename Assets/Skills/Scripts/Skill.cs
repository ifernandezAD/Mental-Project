using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    protected Health health;

    [Header("Stamina")]
    private int maxStamina = 0;
    private int currentStamina = 0;
    private TextMeshProUGUI staminaText;
    private Button rayButton;

    private void Awake()
    {
        InternalAwake();
    }

    protected virtual void InternalAwake()
    {
        maxStamina = GetComponent<CardDisplay>().card.staminaCost;
        staminaText = GetComponent<CardDisplay>().staminaText;
        rayButton = GetComponentInChildren<Button>(true);
        rayButton.onClick.AddListener(OnRayButtonClicked);
        health = GetComponent<Health>();
    }

    private void Start()
    {
        InternalStart();
    }

    private void OnEnable()
    {
        PlayerPhase.onPlayerPhaseBegin += OnPlayerPhaseBegin;
        PlayerPhase.onPlayerPhaseEnded += OnPlayerPhaseEnded;
    }

    private void OnDisable()
    {
        PlayerPhase.onPlayerPhaseBegin -= OnPlayerPhaseBegin;
        PlayerPhase.onPlayerPhaseEnded -= OnPlayerPhaseEnded;
    }

    protected virtual void InternalStart()
    {
        if (GetComponent<CardDisplay>().card.cardType == Card.CardType.Ally)
        {
            AddSlot();
        }

        currentStamina = 0;
        UpdateStaminaDisplay();
        DeactivateRayButton();
    }

    void AddSlot()
    {
        Slots.instance.AddSlot();
    }

    public void IncreaseStamina(int stamina)
    {
        currentStamina += stamina;

        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            ActivateRayButton();
        }

        UpdateStaminaDisplay();
    }

    public void DecreaseStamina(int stamina)
    {
        currentStamina -= stamina;

        if (currentStamina < 0)
        {
            currentStamina = 0;
        }

        if (currentStamina < maxStamina)
        {
            DeactivateRayButton();
        }

        UpdateStaminaDisplay();
    }

    public void MaxOutStamina()
    {
        currentStamina = maxStamina;
        UpdateStaminaDisplay();
    }

    public void ResetStamina()
    {
        currentStamina = 0;
        UpdateStaminaDisplay();
        DeactivateRayButton();
    }

    public virtual void TriggerSkill()
    {
        ResetStamina();
    }

    private void OnRayButtonClicked()
    {
        TriggerSkill();
    }

    private void ActivateRayButton()
    {
        rayButton.gameObject.SetActive(true);
        staminaText.gameObject.SetActive(false);
    }

    private void DeactivateRayButton()
    {
        rayButton.gameObject.SetActive(false);
        staminaText.gameObject.SetActive(true);
    }

    private void OnPlayerPhaseBegin()
    {
        if (currentStamina >= maxStamina)
        {
            ActivateRayButton();
        }
    }

    private void OnPlayerPhaseEnded()
    {
        DeactivateRayButton();
    }

    private void UpdateStaminaDisplay()
    {
        staminaText.text = currentStamina.ToString();
    }
}
