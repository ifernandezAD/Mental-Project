using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    [SerializeField] InputActionReference escapeGame;

    private void OnEnable() { escapeGame.action.Enable(); }

    private void Update() { UpdateEscapeGame(); }

    private void UpdateEscapeGame()
    {
        if (escapeGame.action.WasPerformedThisFrame())
        {
            Application.Quit();
        }
    }
    private void OnDisable() { escapeGame.action.Disable(); }

}
