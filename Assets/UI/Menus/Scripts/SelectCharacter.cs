using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    public void SaveCharacterIndex(int characterIndex)
    {
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);   
        StartGame();
    }

    private void StartGame()
    {
        LoadingScreen.LoadScene(sceneToLoad);
    }

}
