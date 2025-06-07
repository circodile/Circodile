using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance; // 1 solo ui

    public Transform itemsParent; //ASIGNAR PADRE EN INSPECTOR!!!
    InventorySlot[] slots;

    void Awake()
    {
        instance = this; 
    }

    void Start()
    {
       
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI(); 
    }

    // Actualiza la interfaz del inventario
    public void UpdateUI()
    {
        // Obtiene todos los slots hijos del contenedor
        InventorySlot[] slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        // Actualiza cada slot según los ítems del inventario
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventory.instance.items.Count)
            {
                slots[i].AddItem(Inventory.instance.items[i]); // Muestra el ítem
            }
            else
            {
                slots[i].ClearSlot(); // Limpia el slot si no hay ítem
            }
        }
    }
}
