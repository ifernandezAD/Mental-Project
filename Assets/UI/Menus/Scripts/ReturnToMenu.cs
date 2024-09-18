using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    [SerializeField] string sceneToLoad = "MainMenu";

    public void LoadMenu()
    {
        LoadingScreen.LoadScene(sceneToLoad);
    }
}
