using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float interactionRadius = 1f; // Radio para interactuar
    public KeyCode interactionKey = KeyCode.E; // Tecla para esconderse
    public Transform hidePosition; // Posici�n donde se esconde el jugador (opcional)

    [Header("Feedback")]
    public GameObject interactionPrompt; // UI que muestra "Presiona E para esconderte"
    public bool debugMode = false;

    private bool playerInRange = false;

    private void Start()
    {
        // Desactivar el prompt al inicio
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        // Mostrar/ocultar UI de interacci�n
        if (playerInRange && interactionPrompt != null)
        {
            interactionPrompt.SetActive(true);
        }
        else if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Verificar interacci�n
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            PlayerManager.Instance.TryHide(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            playerInRange = true;
            if (debugMode) Debug.Log("Jugador en rango para esconderse");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            playerInRange = false;
            if (debugMode) Debug.Log("Jugador sali� del rango");

            // Si el jugador estaba escondido aqu�, forzar salir
            if (PlayerManager.Instance.isHiding && PlayerManager.Instance.currentHidingSpot == this.transform)
            {
                PlayerManager.Instance.TryHide(this);
            }
        }
    }

    // Dibujar gizmo en el editor para ver el �rea de interacci�n
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
