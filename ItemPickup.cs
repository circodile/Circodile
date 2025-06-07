using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // Asignar el ScriptableObject del �tem en el Inspector!!!

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si es el jugador
        {
            bool wasPickedUp = Inventory.instance.AddItem(item); // Intenta a�adir objeto con el que colisiono al inventario

            if (wasPickedUp)
            {
                Destroy(gameObject); // Elimina el objeto de la escena si se recogi�
            }
        }
    }
}
