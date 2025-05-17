using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Cambia esto por tu escena de juego
    }

    public void QuitGame()
    {
        Application.Quit(); // Solo funciona en builds, no en el editor
    }
}