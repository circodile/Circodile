using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class CambioDeEscenaTrigger : MonoBehaviour
{
    [Tooltip("Nombre sig escena")]
    public string nombreEscenaDestino;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            if (!string.IsNullOrEmpty(nombreEscenaDestino))
            {
                Debug.Log("Cambiando a la escena: " + nombreEscenaDestino);
                SceneManager.LoadScene(nombreEscenaDestino);
            }
            else
            {
                Debug.LogWarning("No se asignó un nombre de escena en " + gameObject.name);
            }
        }
    }
}
