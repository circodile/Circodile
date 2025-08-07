using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Singleton pattern
    public static PlayerManager Instance { get; private set; }

    [Header("Player States")]
    public bool isHiding = false;
    public Transform currentHidingSpot;
    public bool isRunning = false;

    [Header("Movement Settings")]
    public float runningSpeedMultiplier = 2f; 

    private Jugador playerMovement;
    private Alimentos playerNeeds;
    private vidas_jugador playerHealth;
    private float originalSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerMovement = GetComponent<Jugador>();
        playerNeeds = GetComponent<Alimentos>();
        playerHealth = GetComponent<vidas_jugador>();

        
        originalSpeed = playerMovement.velocidadNormal;
    }

    private void Update()
    {
        HandleRunning();
        CheckNeeds();
    }

    private void HandleRunning()
    {
        
        if (Input.GetKey(KeyCode.LeftShift) &&
            (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            if (!isRunning)
            {
                StartRunning();
            }
        }
        else
        {
            if (isRunning)
            {
                StopRunning();
            }
        }
    }

    private void StartRunning()
    {
        isRunning = true;
        playerMovement.velocidadActual = playerMovement.velocidadNormal * runningSpeedMultiplier;
    }

    private void StopRunning()
    {
        isRunning = false;
        playerMovement.velocidadActual = playerMovement.velocidadNormal;
    }

    public void InteractWithResource(Item resource)
    {
        if (resource != null && resource.type == ItemType.Resource)
        {
            bool added = Inventory.instance.AddItem(resource);
            if (added)
            {
                Debug.Log("Recurso recolectado: " + resource.name);
            }
        }
    }

    public void Die()
    {
        playerHealth.PerderVida();
    }

    private void CheckNeeds()
    {
        if (playerNeeds != null &&
            (playerNeeds.sistemaHambre.hambre <= 0 || playerNeeds.sistemaSed.sed <= 0))
        {
            Die();
        }
    }


    public void TryHide(HidingSpot spot)
    {
        if (!isHiding)
        {
            Hide(spot);
        }
        else
        {
            Unhide();
        }
    }

    private void Hide(HidingSpot spot)
    {
        isHiding = true;
        currentHidingSpot = spot.transform;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        playerMovement.enabled = false;

        Debug.Log("Jugador escondido");
    }

    private void Unhide()
    {
        isHiding = false;


        if (currentHidingSpot != null)
        {
            transform.position = currentHidingSpot.position + new Vector3(1f, 0, 0);
        }

        // Reactivar componentes
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        playerMovement.enabled = true;

        currentHidingSpot = null;
    }
}