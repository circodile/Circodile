using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [Header("Referencias persistentes")]
    public GameObject player;
    public GameObject crocodile;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (player != null) DontDestroyOnLoad(player);
        if (crocodile != null) DontDestroyOnLoad(crocodile);
    }

    public void ChangeSceneAdditive(string sceneName, Vector3 newPlayerPos)
    {
        // Cargar nueva escena sin descargar la actual
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        // Mover jugador
        if (player != null)
            player.transform.position = newPlayerPos;
    }
}
