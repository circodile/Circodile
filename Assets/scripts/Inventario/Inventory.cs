using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public TextMeshProUGUI mensajeTexto;

    public List<Item> items = new List<Item>();
    public int maxSlots = 4; // límite de objetos
    public float mensajeDuracion = 5f; // duración de los mensajes en pantalla

    // Variables para manejo de mensajes acumulativos
    private Coroutine mensajeCoroutine;
    private string mensajesAcumulados = "";

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
            MostrarMensajeTemporal("Inventario lleno!, Deja tus objetos en el barco.");
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

        // 5. Mostrar mensaje en pantalla si fue removido correctamente
        if (removalSuccess)
        {
            Debug.Log($"Se removió {itemToRemove.name} del inventario");
        }
        else
        {
            Debug.LogError($"Error inesperado al remover {itemToRemove.name}");
        }

        return removalSuccess;
    }

    // ---------- COROUTINE PARA MENSAJES ----------
    private void MostrarMensajeTemporal(string texto)
    {
        // Acumula todos los mensajes
        mensajesAcumulados += texto + "\n";

        // Reinicia la coroutine si ya estaba corriendo
        if (mensajeCoroutine != null)
            StopCoroutine(mensajeCoroutine);

        mensajeCoroutine = StartCoroutine(MostrarMensajeCoroutine());
    }

    private IEnumerator MostrarMensajeCoroutine()
    {
        mensajeTexto.text = mensajesAcumulados;
        yield return new WaitForSeconds(mensajeDuracion);
        mensajesAcumulados = "";
        mensajeTexto.text = "";
        mensajeCoroutine = null;
    }
}
