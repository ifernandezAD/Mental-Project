using UnityEngine;
using UnityEngine.UI;

public class SymbolSpawner : MonoBehaviour
{
    [Header("Symbol Settings")]
    public ImageType imageType;   
    public GameObject prefab;     

    [Header("Event Settings")]
    private Button spawnButton;    

    private void Awake()
    {
        spawnButton=GetComponent<Button>();
        spawnButton.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        Roller.instance.AddImagePrefab(imageType, prefab);

        EventManager.instance.EventButtonPressed();

        EventManager.instance.gameObject.SetActive(false);
    }
}
