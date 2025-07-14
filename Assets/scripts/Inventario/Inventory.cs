using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // Para llamadas + faciles y que cualquiera lo pueda usar

    public List<Item> items = new List<Item>(); 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }                            // Configura el patrón Singleton (solo un inventario)
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(Item newItem)
    {
        Debug.Log("Agregado al inventario: " + newItem.name);

        if (!newItem.stackable)
        {
            items.Add(newItem); 
        }
        else
        {
            // Para stackear items
            items.Add(newItem);
        }
        InventoryUI.instance?.UpdateUI(); 
        return true;
    }

    
    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        InventoryUI.instance.UpdateUI(); 
    }
}
