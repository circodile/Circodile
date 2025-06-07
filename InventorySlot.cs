using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; 
    public Item item;  

    // Añade un ítem al slot
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.image; // Asigna el icono
        icon.enabled = true;    // Muestra la imagen
    }

    // Limpia el slot
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false; 
    }
}
