using UnityEngine;

public class vidas_jugador : MonoBehaviour
{
    public int vidas = 3; // N�mero de vidas del jugador

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el jugador colisiona con un objeto que tiene la etiqueta "Enemigo"
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Resta una vida al jugador
            vidas--;
            // Verifica si el jugador se queda sin vidas
            if (vidas <= 0)
            {
                // Aqu� puedes agregar la l�gica para manejar la muerte del jugador
                Destroy(gameObject); // Destruye el objeto del jugador
            }
        }
    }
}
