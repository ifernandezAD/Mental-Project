using UnityEngine;

public class TestingAddRemoveIcons : MonoBehaviour
{
    [SerializeField] GameObject poisonIconPrefab;

    [Header("Testing")]
    [SerializeField] bool testAddIcon;
    [SerializeField] bool testRemoveIcon;

    void OnValidate()
    {
        if (testAddIcon)
        {
            AddIcon();
            testAddIcon = false;
        }

        if (testRemoveIcon)
        {
            RemoveIcon();
            testRemoveIcon = false;
        }
    }

    void AddIcon()
    {
        Roller.instance.AddImagePrefab(ImageType.Poison,poisonIconPrefab);
    }



    void RemoveIcon()
    {
        Roller.instance.RemoveImagePrefab(ImageType.Sword);
    }

}
