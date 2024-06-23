using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Loto : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] bool testPrintLotoPool;
    [SerializeField] bool testSubstractFromPool;

    [SerializeField] bool testRefillPool;
    [SerializeField] bool testIncreasePoolSize;
    [SerializeField] bool testDecreasePoolSize;

    [SerializeField] CharacterController characterController;
    [SerializeField] private int maxValue;
    private List<int> lottoPool;

    void OnValidate()
    {
        if (testPrintLotoPool)
        {
            TestingPrintLottoPool();
            testPrintLotoPool = false;
        }

        if (testSubstractFromPool)
        {
            SubtractFromPool();
            testSubstractFromPool = false;
        }

        if (testRefillPool)
        {
            RefillPool();
            testRefillPool = false;
        }

        if (testIncreasePoolSize)
        {
            ChangePoolSize(1);
            testIncreasePoolSize = false;
        }

        if (testDecreasePoolSize)
        {
            ChangePoolSize(-1);
            testDecreasePoolSize = false;
        }

    }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        //maxValue = characterController.selectedCharacter.happinessRange;
        lottoPool = new List<int>();

        for (int i = 1; i <= maxValue; i++)
        {
            lottoPool.Add(i);
        }
    }

    public void SubtractFromPool()
    {
        if (lottoPool.Count == 0)
        {
            Debug.LogWarning("The lotto pool is empty.");
            RefillPool();
            return;
        }

        int randomIndex = Random.Range(0, lottoPool.Count);
        int randomValue = lottoPool[randomIndex];
        lottoPool.RemoveAt(randomIndex);
        Debug.Log("Value " + randomValue + " removed from the pool.");
    }

    void RefillPool()
    {
        lottoPool.Clear();
        for (int i = 1; i <= maxValue; i++)
        {
            lottoPool.Add(i);
        }
    }

    void ChangePoolSize(int value)
    {
        maxValue += value;
    }

    void TestingPrintLottoPool()
    {
        foreach (int value in lottoPool)
        {
            Debug.Log(value);
        }
    }

}
