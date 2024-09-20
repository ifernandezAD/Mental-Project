using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-150)]
public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    [Header("References")]
    [SerializeField] Transform characterContainer;
    public GameObject mainCharacterCard; 
    [SerializeField] public GameObject[] availableCharacters; 
    [SerializeField] public Transform bubblesContainer;

    [Header("Ally Related Variables")]
    [SerializeField] public Transform allyContainer;
    [SerializeField] GameObject[] allyArray;
    private List<GameObject> remainingAllies;

    void Awake()
    {
        instance = this;
        remainingAllies = new List<GameObject>(allyArray);
    }

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); 
        GameObject selectedCharacter = Instantiate(availableCharacters[selectedCharacterIndex], characterContainer);
        mainCharacterCard = selectedCharacter;
    }

    void OnEnable()
    {
        //AllyEvent.onAllyObtained += AddAlly;
    }

    private void AddAlly(int allyIndex)
    {
        if (allyIndex >= 0 && allyIndex < remainingAllies.Count)
        {
            GameObject selectedAlly = remainingAllies[allyIndex];
            GameObject newAlly = Instantiate(selectedAlly, allyContainer);

            remainingAllies.RemoveAt(allyIndex);
        }
        else
        {
            Debug.LogWarning("Índice de aliado fuera de rango o inválido.");
        }
    }

    void OnDisable()
    {
        //AllyEvent.onAllyObtained -= AddAlly;
    }
}
