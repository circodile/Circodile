using UnityEngine;
using UnityEngine.UI;

public class BoatManager : MonoBehaviour
{
    [System.Serializable]
    public class BoatPart
    {
        public string partName;
        public Item requiredItem;
        public int requiredAmount;
        public int currentAmount;
        public Image partImage;
        public GameObject visualIndicator;
        public Text progressText;
    }

    [Header("Partes del bote")]
    public BoatPart[] boatParts;

    [Header("Configuración")]
    public float interactionDistance = 3f;
    public KeyCode buildKey = KeyCode.F;

    [Header("UI de victoria")]
    public GameObject winCanvas; // <- asigna aquí tu Canvas "Ganaste"

    private Transform player;
    private bool isPlayerNear;

    private void Start()
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("No se encontró Inventory.instance");
            enabled = false;
            return;
        }

        if (PlayerManager.Instance == null)
        {
            Debug.LogError("No se encontró PlayerManager.Instance");
            enabled = false;
            return;
        }

        player = PlayerManager.Instance.transform;

        foreach (var part in boatParts)
        {
            part.currentAmount = Mathf.Clamp(part.currentAmount, 0, part.requiredAmount);
            UpdatePartVisuals(part);

            if (part.visualIndicator != null)
                part.visualIndicator.SetActive(false);
            else
                Debug.LogWarning($"visualIndicator no asignado para {part.partName}");
        }

        // Aseguramos que el canvas esté apagado al iniciar
        if (winCanvas != null)
            winCanvas.SetActive(false);
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        isPlayerNear = distance <= interactionDistance;

        foreach (var part in boatParts)
        {
            if (part.visualIndicator != null)
                part.visualIndicator.SetActive(isPlayerNear && !IsPartComplete(part));
        }

        if (isPlayerNear && Input.GetKeyDown(buildKey))
        {
            TryBuild();
        }
    }

    private void TryBuild()
    {
        bool builtSomething = false;

        foreach (var part in boatParts)
        {
            if (!IsPartComplete(part))
            {
                int itemCount = CountItemsInInventory(part.requiredItem);
                int needed = part.requiredAmount - part.currentAmount;

                if (itemCount > 0 && needed > 0)
                {
                    int toAdd = Mathf.Min(itemCount, needed);

                    if (ConsumeItemsFromInventory(part.requiredItem, toAdd))
                    {
                        part.currentAmount += toAdd;
                        UpdatePartVisuals(part);
                        Debug.Log($"Añadidos {toAdd} {part.requiredItem.name} al bote ({part.currentAmount}/{part.requiredAmount})");

                        if (IsPartComplete(part))
                            Debug.Log($"Parte {part.partName} completada");

                        builtSomething = true;
                    }
                }
            }
        }

        if (IsBoatComplete())
        {
            Debug.Log("¡Bote completado!");
            if (winCanvas != null)
                winCanvas.SetActive(true); // <- activamos el Canvas
            Time.timeScale = 0f; // <- pausa el juego
            Debug.Log(" Mostrando pantalla de victoria...");
        }
        else if (!builtSomething)
        {
            Debug.Log("No tienes recursos suficientes para construir ninguna parte");
        }
    }

    private int CountItemsInInventory(Item item)
    {
        if (Inventory.instance == null) return 0;

        int count = 0;
        foreach (var inventoryItem in Inventory.instance.items)
        {
            if (inventoryItem != null && inventoryItem.name == item.name)
                count++;
        }
        return count;
    }

    private bool ConsumeItemsFromInventory(Item item, int amount)
    {
        if (Inventory.instance == null) return false;

        int removed = 0;
        for (int i = 0; i < amount; i++)
        {
            Item itemToRemove = Inventory.instance.items.Find(x => x != null && x.name == item.name);
            if (itemToRemove != null)
            {
                Inventory.instance.RemoveItem(itemToRemove);
                removed++;
            }
            else break;
        }
        return removed == amount;
    }

    private bool IsPartComplete(BoatPart part)
    {
        return part.currentAmount >= part.requiredAmount;
    }

    private bool IsBoatComplete()
    {
        foreach (var part in boatParts)
        {
            if (!IsPartComplete(part))
                return false;
        }
        return true;
    }

    private void UpdatePartVisuals(BoatPart part)
    {
        if (part.progressText != null)
            part.progressText.text = $"{part.currentAmount}/{part.requiredAmount}";

        if (part.partImage != null && part.requiredItem != null)
            part.partImage.sprite = part.requiredItem.image;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
