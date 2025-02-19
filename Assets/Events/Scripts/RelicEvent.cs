using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicEvent : GenericEvent
{
    [SerializeField] RelicObject[] relicsPool;
    [SerializeField] GameObject[] relicsPrefabs;
    [SerializeField] GameObject[] relicsDummies;
    [SerializeField] RelicObject relicBad;
    [SerializeField] GameObject relicBadPrefab;
    private Transform relicContainer;

    private void Awake()
    {
        relicContainer = GameLogic.instance.relicContainer;
    }

    public override void Initialize(bool isFlashback)
    {
        base.Initialize(isFlashback);
        SelectRandomRelicObjects();
    }

    private void OnDisable()
    {
        ActivateDummies(false);
    }

    private void ActivateDummies(bool isActive, int activeCount = 0)
    {
        for (int i = 0; i < relicsDummies.Length; i++)
        {
            relicsDummies[i].SetActive(isActive && i < activeCount);
        }
    }

    private void SelectRandomRelicObjects()
    {
        if (IsFlashback && !IsGoodFlashback) 
        {
            SetBadDummyRelic(relicsDummies[0], relicBad);
            ActivateDummies(true, 1); 
            return;
        }

        int relicsToSelect = (IsFlashback && IsGoodFlashback) ? 3 : 1;

        if (relicsPool.Length < relicsToSelect)
        {
            Debug.LogWarning("No hay suficientes reliquias en el pool.");
            return;
        }

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < relicsPool.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < relicsToSelect; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            int selectedRelicIndex = availableIndices[randomIndex];

            SetDummyRelic(relicsDummies[i], relicsPool[selectedRelicIndex]);

            availableIndices.RemoveAt(randomIndex);
        }

        ActivateDummies(true, relicsToSelect);
    }

    private void SetDummyRelic(GameObject dummy, RelicObject relic)
    {
        RelicDisplay relicDisplay = dummy.GetComponent<RelicDisplay>();

        relicDisplay.relicObject = relic;
        relicDisplay.relicArt.sprite = relic.relicArt;

        Button dummyButton = dummy.GetComponent<Button>();
        dummyButton.onClick.RemoveAllListeners();
        dummyButton.onClick.AddListener(() => OnRelicClick(relic));
    }

    private void SetBadDummyRelic(GameObject dummy, RelicObject badRelic)
    {
        RelicDisplay relicDisplay = dummy.GetComponent<RelicDisplay>();

        relicDisplay.relicObject = badRelic;
        relicDisplay.relicArt.sprite = badRelic.relicArt;

        Button dummyButton = dummy.GetComponent<Button>();
        dummyButton.onClick.RemoveAllListeners();
        dummyButton.onClick.AddListener(() => OnRelicClick(badRelic));
    }

    public void OnRelicClick(RelicObject relic)
    {
        GameObject selectedPrefab;

        if (relic == relicBad)
        {
            selectedPrefab = relicBadPrefab;
        }
        else
        {
            int relicIndex = relic.index;

            if (relicIndex >= 0 && relicIndex < relicsPrefabs.Length)
            {
                selectedPrefab = relicsPrefabs[relicIndex];
            }
            else
            {
                Debug.LogWarning("Índice de reliquia fuera de rango o inválido.");
                return;
            }
        }

        Instantiate(selectedPrefab, relicContainer);
        Debug.Log("Relic instantiated: " + selectedPrefab.name);
    }
}
