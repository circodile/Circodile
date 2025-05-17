using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Referencias a los paneles (arrastra desde el editor)
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    // M�todo para el bot�n "Opciones"
    public void ShowOptions()
    {
        mainMenuPanel.SetActive(false);  // Oculta el men� principal
        optionsPanel.SetActive(true);    // Muestra el panel de opciones
    }

    // M�todo para el bot�n "Atr�s"
    public void ShowMainMenu()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}