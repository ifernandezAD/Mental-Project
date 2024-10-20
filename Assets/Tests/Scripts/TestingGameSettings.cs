using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class TestingGameSettings : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] TMP_InputField actInputField;
    [SerializeField] TMP_InputField roundInputField;

    public void StartGameWithTesting()
    {
        int selectedAct = int.Parse(actInputField.text);
        int selectedRound = int.Parse(roundInputField.text);

        PlayerPrefs.SetInt("Testing_Act", selectedAct);
        PlayerPrefs.SetInt("Testing_Round", selectedRound);
        PlayerPrefs.Save();

        LoadingScreen.LoadScene(sceneToLoad);
    }

}
