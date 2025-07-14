using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // Asignar el ScriptableObject del ítem en el Inspector!!!

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entró en trigger con: " + other.name);
        if (other.CompareTag("Jugador")) // Verifica si es el jugador
        {
            Debug.Log("Tag correcto: Jugador");
            bool wasPickedUp = Inventory.instance.AddItem(item); // Intenta añadir objeto con el que colisiono al inventario

            if (wasPickedUp)
            {
                Destroy(gameObject); // Elimina el objeto de la escena si se recogió
            }
        }
    }
}
