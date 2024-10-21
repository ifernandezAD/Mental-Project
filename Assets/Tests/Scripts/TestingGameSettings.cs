using UnityEngine;
using TMPro;

public class TestingGameSettings : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] TMP_InputField actInputField;
    [SerializeField] TMP_InputField roundInputField;
    [SerializeField] TMP_InputField characterInputField; 
    [SerializeField] TMP_InputField alliesInputField; 
    [SerializeField] TMP_InputField consumablesInputField; 
    [SerializeField] TMP_InputField relicsInputField; 
    [SerializeField] TMP_InputField symbolsInputField; 

    public void StartGameWithTesting()
    {
        int selectedAct = TryParseInputField(actInputField, 0);
        int selectedRound = TryParseInputField(roundInputField, 0);
        PlayerPrefs.SetInt("Testing_Act", selectedAct);
        PlayerPrefs.SetInt("Testing_Round", selectedRound);

        int selectedCharacter = TryParseInputField(characterInputField, 0);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);

            int numberOfAllies = TryParseInputField(alliesInputField, 0); 
        PlayerPrefs.SetInt("Testing_Allies", numberOfAllies); 

        int numberOfConsumables = TryParseInputField(consumablesInputField, 0);
        PlayerPrefs.SetInt("Testing_Consumables", numberOfConsumables);

        int numberOfRelics = TryParseInputField(relicsInputField, 0);
        PlayerPrefs.SetInt("Testing_Relics", numberOfRelics);

        int numberOfSymbols = TryParseInputField(symbolsInputField, 0);
        PlayerPrefs.SetInt("Testing_Symbols", numberOfSymbols);

        PlayerPrefs.Save();

        LoadingScreen.LoadScene(sceneToLoad);
    }

    private int TryParseInputField(TMP_InputField inputField, int defaultValue)
    {
        if (int.TryParse(inputField.text, out int result))
        {
            return result;
        }
        return defaultValue; 
    }
}
