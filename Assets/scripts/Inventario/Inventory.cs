using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> items = new List<Item>();
    public int maxSlots = 4; // ← limite de objetos

    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
            
    }

    public bool AddItem(Item newItem)
    {
        if (GetTotalItemCount() >= maxSlots)
        {
            Debug.Log("Inventario lleno!");
            return false;
        }

        items.Add(newItem);
        InventoryUI.instance?.UpdateUI();
        return true;
    }

    private int GetTotalItemCount()
    {
        return items.Count; // Cuenta todos los items, incluyendo duplicados
    }

    public bool RemoveItem(Item itemToRemove)
    {
        // 1. Verificar parámetros nulos
        if (itemToRemove == null)
        {
            Debug.LogWarning("Intento de remover un item nulo");
            return false;
        }

        // 2. Verificar si el item existe en el inventario
        if (!items.Contains(itemToRemove))
        {
            Debug.LogWarning($"El item {itemToRemove.name} no está en el inventario");
            return false;
        }

        // 3. Remover el item
        bool removalSuccess = items.Remove(itemToRemove);

        // 4. Actualizar UI si existe
        if (InventoryUI.instance != null)
        {
            InventoryUI.instance.UpdateUI();
        }
        else
        {
            Debug.LogWarning("No hay instancia de InventoryUI para actualizar");
        }

        // 5. Retornar éxito/fracaso
        if (!removalSuccess)
        {
            Debug.LogError($"Error inesperado al remover {itemToRemove.name}");
        }

        return removalSuccess;
    }
}

