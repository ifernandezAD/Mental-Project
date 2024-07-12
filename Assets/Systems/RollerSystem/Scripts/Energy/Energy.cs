using System;
using TMPro;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public static Energy instance { get; private set; }
   
    [Header("Energy Variables")]
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] int maxEnergy = 3;
    [SerializeField] int currentEnergy = 3;
    public static Action onOutOfEnergy;
    public static Action onResetEnergy;

    [Header("Testing Energy")]
    [SerializeField] bool testAddEnergy;
    [SerializeField] bool testRemoveEnergy;
    [SerializeField] bool testResetEnergy;

    void OnValidate()
    {
        if (testAddEnergy)
        {
            ChangeMaxEnergy(1);
            testAddEnergy = false;
        }

        if (testRemoveEnergy)
        {
            ChangeMaxEnergy(-1);
            testRemoveEnergy = false;
        }

        if (testResetEnergy)
        {
            ResetEnergy();
            testResetEnergy = false;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        energyText.text = "Energy: " + maxEnergy;
    }

        public void ChangeMaxEnergy(int energy)
    {
        maxEnergy += energy;
    }

    public void RemoveEnergy()
    {
        currentEnergy--;
        energyText.text = "Energy: " + currentEnergy;
    }
    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        energyText.text = "Energy: " + maxEnergy;

        onResetEnergy?.Invoke();
    }

    public void TriggerOnOutOfEnergy()
    {
        onOutOfEnergy?.Invoke();
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

}
