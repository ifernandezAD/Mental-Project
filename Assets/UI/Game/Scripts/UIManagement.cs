using UnityEngine;

public class UIManagement : MonoBehaviour
{
    public static UIManagement instance { get; private set; }

    [Header("Act Image Container")]
    [SerializeField] private Transform actImageContainer;  

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetActiveActImage(0);  
    }

    
    public void SetActiveActImage(int actNumber)
    {
        for (int i = 0; i < actImageContainer.childCount; i++)
        {
            actImageContainer.GetChild(i).gameObject.SetActive(i == actNumber);  
        }
    }
}
