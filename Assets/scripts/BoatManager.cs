using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;  

public class BoatManager : MonoBehaviour
{
    public TextMeshProUGUI mensajeTexto;

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

    [Header("Configuraci�n")]
    public float interactionDistance = 3f;
    public KeyCode buildKey = KeyCode.F;
    public float mensajeDuracion = 5f; // Duraci�n de los mensajes en pantalla

    private Transform player;
    private bool isPlayerNear;

    // Variables para manejo de mensajes acumulativos
    private Coroutine mensajeCoroutine;
    private string mensajesAcumulados = "";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Guarda el progreso del barco
    }

    private void Start()
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("No se encontr� Inventory.instance");
            enabled = false;
            return;
        }

        if (PlayerManager.Instance == null)
        {
            Debug.LogError("No se encontr� PlayerManager.Instance");
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

                        // Mensaje acumulativo en pantalla
                        MostrarMensajeTemporal($"A�adidos {toAdd} {part.requiredItem.name} al barco ({part.currentAmount}/{part.requiredAmount})");

                        if (IsPartComplete(part))
                            MostrarMensajeTemporal($"Parte {part.partName} completada");

                        builtSomething = true;
                    }
                }
            }
        }

        if (IsBoatComplete())
        {
             
            SceneManager.LoadScene("Gano");
        }
        else if (!builtSomething)
        {
            MostrarMensajeTemporal("No tienes recursos suficientes para construir");
        }
    }

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

        for (int i = 0; i < amount; i++)
        {
            Item itemToRemove = Inventory.instance.items.Find(x => x != null && x.name == item.name);
            if (itemToRemove == null) return false;

            if (!Inventory.instance.RemoveItem(itemToRemove))
            {
                Debug.LogWarning($"Fallo al remover {item.name}");
                return false;
            }
        }
        return true;
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
