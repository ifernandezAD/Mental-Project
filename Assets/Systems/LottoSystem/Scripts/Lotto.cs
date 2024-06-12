using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Loto : MonoBehaviour
{
    [SerializeField] private Happiness happiness;

    [SerializeField] private int maxValue;
    private List<int> lottoPool;

    void Start()
    {

    }
}
