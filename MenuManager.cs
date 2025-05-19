using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Referencias a los paneles (arrastra en el Inspector)
    public GameObject MainMenuPanel;
    public GameObject OptionPanel; // Usa el nombre exacto de tu jerarquía
    public GameObject PauseMenu;   // Respeta "PauseMenu" sin "Panel"

    // Objetos del juego (opcional: para activar/desactivar al pausar)
    public GameObject Cube;
    public GameObject[] Paredes;

    void Start()
    {
        // Configuración inicial
        MainMenuPanel.SetActive(true);
        OptionPanel.SetActive(false);
        PauseMenu.SetActive(false);

        // Si el juego debe estar inactivo al inicio:
        SetGameplayActive(false);
    }

    // Llamado por el botón "Jugar"
    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        SetGameplayActive(true);
    }

    // Llamado por el botón "Opciones"
    public void ShowOptions()
    {
        MainMenuPanel.SetActive(false);
        OptionPanel.SetActive(true);
    }

    // Llamado por el botón "Atrás" en OptionPanel
    public void BackToMainMenu()
    {
        OptionPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    // Llamado al presionar ESC
    public void TogglePause()
    {
        bool isPaused = !PauseMenu.activeSelf;
        PauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        SetGameplayActive(!isPaused); // Opcional
    }

    // Activa/desactiva objetos del juego
    private void SetGameplayActive(bool active)
    {
        Cube.SetActive(active);
        foreach (GameObject pared in Paredes)
        {
            pared.SetActive(active);
        }
    }
}