using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
     public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void OpenSettings()
    {
        SceneManager.LoadSceneAsync(1);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
