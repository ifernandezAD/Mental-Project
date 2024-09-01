using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-150)]
public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    [Header("References")]
    [SerializeField] public GameObject molePopUp;
    [SerializeField] Transform characterContainer;
    public GameObject mainCharacterCard;

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
        mainCharacterCard = characterContainer.GetChild(0).gameObject;

        molePopUp.SetActive(false);
    }

    void OnEnable()
    {
        EventManager.onAllyObtained += AddRandomAlly;
    }

    private void AddRandomAlly()
    {
        int randomIndex = UnityEngine.Random.Range(0, remainingAllies.Count);
        GameObject selectedAlly = remainingAllies[randomIndex];

        GameObject newAlly = Instantiate(selectedAlly, allyContainer);

        remainingAllies.RemoveAt(randomIndex);
    }

    void OnDisable()
    {
        EventManager.onAllyObtained -= AddRandomAlly;
    }
}
