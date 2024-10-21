using UnityEngine;

public class EnableTestingGameSettings : MonoBehaviour
{
    void Start()
    {
        RoundManager.instance.LoadTestingRoundManagerPreferences();
        
    }

}
