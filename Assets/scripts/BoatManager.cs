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
        public Image partImage; // barra de progreso
        public GameObject visualIndicator;
        public Text progressText; // texto tipo 3 / 5
    }

    [Header("Partes del bote")]
    public BoatPart[] boatParts;

    [Header("Configuración")]
    public float interactionDistance = 2f;
    public KeyCode buildKey = KeyCode.F;

    private Transform player;
    private bool isPlayerNear;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Jugador").transform;

        foreach (var part in boatParts)
        {
            part.currentAmount = 0;
            UpdatePartVisuals(part);
            part.visualIndicator.SetActive(false);
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        isPlayerNear = distance <= interactionDistance;

        
        foreach (var part in boatParts)
        {
            part.visualIndicator.SetActive(isPlayerNear && !IsPartComplete(part));
        }

       
        if (isPlayerNear && Input.GetKeyDown(buildKey))
        {
            TryBuild();
        }
    }

    private void TryBuild()
    {
        foreach (var part in boatParts)
        {
            if (!IsPartComplete(part))
            {
                
                int itemCount = CountItemsInInventory(part.requiredItem);

                if (itemCount > 0)
                {
                    
                    int toAdd = Mathf.Min(1, part.requiredAmount - part.currentAmount);

                    
                    ConsumeItemsFromInventory(part.requiredItem, toAdd);

                   
                    part.currentAmount += toAdd;
                    UpdatePartVisuals(part);

                    Debug.Log($"Añadido {toAdd} {part.requiredItem.name} al bote. Progreso: {part.currentAmount}/{part.requiredAmount}");

                    if (IsPartComplete(part))
                    {
                        Debug.Log($"¡Parte {part.partName} completada!");
                    }

                    return; 
                }
            }
        }

        Debug.Log("No tienes los recursos necesarios o el bote ya está completo");
    }

    private int CountItemsInInventory(Item item)
    {
        int count = 0;
        foreach (var inventoryItem in Inventory.instance.items)
        {
            if (inventoryItem == item)
            {
                count++;
            }
        }
        return count;
    }

    private void ConsumeItemsFromInventory(Item item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Inventory.instance.RemoveItem(item);
        }
    }

    private bool IsPartComplete(BoatPart part)
    {
        return part.currentAmount >= part.requiredAmount;
    }

    private void UpdatePartVisuals(BoatPart part)
    {
        if (part.partImage != null)
        {
            float progress = (float)part.currentAmount / part.requiredAmount;
            part.partImage.fillAmount = progress;
            part.partImage.color = Color.Lerp(Color.red, Color.green, progress);
        }

        if (part.progressText != null)
        {
            part.progressText.text = $"{part.currentAmount} / {part.requiredAmount}";
        }
    }

    public bool IsBoatComplete()
    {
        foreach (var part in boatParts)
        {
            if (!IsPartComplete(part))
            {
                return false;
            }
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }


}
