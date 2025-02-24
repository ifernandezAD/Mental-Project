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
    [SerializeField] public Transform allyContainer;
    [SerializeField] public Transform enemyContainer;
    [SerializeField] public Transform objectContainer;
    [SerializeField] public Transform relicContainer;
    [SerializeField] public DrawPhase drawPhase;

    [Header("BossesEffects")]
    private bool isDamagedLocked = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); 
        GameObject selectedCharacter = Instantiate(availableCharacters[selectedCharacterIndex], characterContainer);
        mainCharacterCard = selectedCharacter;
    }

    #region BossEffects
    public void ActivateDamageLocks() 
    {
        isDamagedLocked = true;
    }
    public void DeactivateDamageLocks()
    {
        isDamagedLocked = false;
    }
    public bool CheckDamageLockStatus() 
    {
        return isDamagedLocked;
    }

    #endregion

}
