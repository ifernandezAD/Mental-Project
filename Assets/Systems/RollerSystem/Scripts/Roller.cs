using System;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    public static Roller instance { get; private set; }

    [Header("Testing Slots")]
    [SerializeField] bool testRoll;

    [Header("Testing Roll Outcome")]
    [SerializeField] bool testOutcome;

    [Header("References")]
    [SerializeField] GameObject slots;

    [Header("Image Prefabs")]
    [SerializeField] List<ImagePrefab> imagePrefabs = new List<ImagePrefab>();

    private Dictionary<ImageType, int> imageCount = new Dictionary<ImageType, int>();

    [Serializable]
    public class ImagePrefab
    {
        public ImageType type;
        public GameObject prefab;
    }

    void OnValidate()
    {
        if (testRoll)
        {
            RollRandomImages();
            testRoll = false;
        }

        if (testOutcome)
        {
            CalculateRollOutcome();
            testOutcome = false;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitializeImageCount();
    }

    void InitializeImageCount()
    {
        imageCount.Clear();
        foreach (ImageType type in Enum.GetValues(typeof(ImageType)))
        {
            imageCount.Add(type, 0);
        }
    }

    public bool IsImageActiveInSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.transform.childCount)
        {
            Transform slot = slots.transform.GetChild(slotIndex);
            Transform icons = slot.GetChild(1);

            foreach (Transform child in icons)
            {
                if (child.gameObject.activeSelf)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void RollRandomImages()
    {
        CheckEnergyForRoll();

        int slotIndex = 0;
        foreach (Transform slot in slots.transform)
        {
            if (slot.gameObject.activeSelf && !Slots.instance.lockedSlots[slotIndex])
            {
                Transform icons = slot.GetChild(1);
                foreach (Transform child in icons)
                {
                    Destroy(child.gameObject);
                }

                InstantiateRandomPrefab(icons);
            }

            slotIndex++;
        }

        UpdateImageCount();
        Energy.instance.RemoveEnergy();

        CalculateRollOutcome(); //Testing
    }

    void CheckEnergyForRoll()
    {
        int currentEnergy = Energy.instance.GetCurrentEnergy();

        if (currentEnergy <= 0)
        {
            return;
        }

        if (currentEnergy == 1)
        {
            Energy.instance.TriggerOnOutOfEnergy();
        }
    }

    void InstantiateRandomPrefab(Transform parent)
    {
        List<ImagePrefab> activePrefabs = GetActivePrefabs();
        if (activePrefabs.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, activePrefabs.Count);
            GameObject instantiatedPrefab = Instantiate(activePrefabs[randomIndex].prefab, parent);
            // Attach ImageTypeComponent to the instantiated prefab
            ImageTypeComponent typeComponent = instantiatedPrefab.AddComponent<ImageTypeComponent>();
            typeComponent.imageType = activePrefabs[randomIndex].type;
        }
    }

    List<ImagePrefab> GetActivePrefabs()
    {
        List<ImagePrefab> activePrefabs = new List<ImagePrefab>();
        foreach (ImagePrefab prefab in imagePrefabs)
        {
            if (prefab.prefab != null)
            {
                activePrefabs.Add(prefab);
            }
        }
        return activePrefabs;
    }

    public void DisableAllSlotImages()
    {
        foreach (Transform slot in slots.transform)
        {
            Transform icons = slot.GetChild(1);
            foreach (Transform icon in icons)
            {
                Destroy(icon.gameObject);
            }
        }
    }

    void UpdateImageCount()
    {
        InitializeImageCount();
        foreach (Transform slot in slots.transform)
        {
            if (slot.gameObject.activeSelf)
            {
                Transform icons = slot.GetChild(1);
                foreach (Transform icon in icons)
                {
                    ImageType imageType = GetImageType(icon.gameObject);
                    if (imageType != ImageType.None)
                    {
                        imageCount[imageType]++;
                    }
                }
            }
        }
    }

    ImageType GetImageType(GameObject imageObject)
    {
        ImageTypeComponent typeComponent = imageObject.GetComponent<ImageTypeComponent>();
        if (typeComponent != null)
        {
            return typeComponent.imageType;
        }
        return ImageType.None;
    }

    public int GetImageCount(ImageType type)
    {
        if (imageCount.ContainsKey(type))
        {
            return imageCount[type];
        }
        return 0;
    }

    void CalculateRollOutcome()
    {
        foreach (KeyValuePair<ImageType, int> pair in imageCount)
        {
            Debug.Log($"{pair.Key} count is {pair.Value}");
        }
    }

    public void AddImagePrefab(ImageType type, GameObject prefab)
    {
        ImagePrefab newPrefab = new ImagePrefab { type = type, prefab = prefab };
        imagePrefabs.Add(newPrefab);
    }

    public void RemoveImagePrefab(ImageType type)
    {
        ImagePrefab prefabToRemove = imagePrefabs.Find(p => p.type == type);
        if (prefabToRemove != null)
        {
            imagePrefabs.Remove(prefabToRemove);
        }
    }
}
