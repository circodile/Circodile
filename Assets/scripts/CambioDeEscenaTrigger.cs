using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class CambioDeEscenaTrigger : MonoBehaviour
{
    [Tooltip("Nombre de la escena de destino")]
    public string nombreEscenaDestino;

    [Tooltip("Posición donde aparecerá el jugador en la nueva escena")]
    public Vector3 posicionSpawnJugador;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            if (!string.IsNullOrEmpty(nombreEscenaDestino))
            {
                Debug.Log("Cargando escena aditiva: " + nombreEscenaDestino);
                SceneTransitionManager.Instance.ChangeSceneAdditive(nombreEscenaDestino, posicionSpawnJugador);
            }
            else
            {
                Debug.LogWarning("No se asignó un nombre de escena en " + gameObject.name);
            }
        }
    }
}
