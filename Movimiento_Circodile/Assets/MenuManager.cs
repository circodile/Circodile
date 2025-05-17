using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Referencias a los paneles (arrastra desde el editor)
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    // Método para el botón "Opciones"
    public void ShowOptions()
    {
        mainMenuPanel.SetActive(false);  // Oculta el menú principal
        optionsPanel.SetActive(true);    // Muestra el panel de opciones
    }

    // Método para el botón "Atrás"
    public void ShowMainMenu()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}