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
        int selectedAct = int.Parse(actInputField.text);
        int selectedRound = int.Parse(roundInputField.text);
        PlayerPrefs.SetInt("Testing_Act", selectedAct);
        PlayerPrefs.SetInt("Testing_Round", selectedRound);

        
        int selectedCharacter = int.Parse(characterInputField.text);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);

        
        int numberOfConsumables = int.Parse(consumablesInputField.text);
        PlayerPrefs.SetInt("Testing_Consumables", numberOfConsumables);

        
        int numberOfRelics = int.Parse(relicsInputField.text);
        PlayerPrefs.SetInt("Testing_Relics", numberOfRelics);

        int numberOfSymbols = int.Parse(symbolsInputField.text);
        PlayerPrefs.SetInt("Testing_Symbols", numberOfSymbols);

        
        string alliesToSpawn = alliesInputField.text;
        PlayerPrefs.SetString("Testing_Allies", alliesToSpawn);

        PlayerPrefs.Save();

    
        LoadingScreen.LoadScene(sceneToLoad);
    }

}
