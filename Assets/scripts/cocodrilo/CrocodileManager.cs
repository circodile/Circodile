using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CrocodileManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float updateInterval = 5f; // Tiempo en segundos para actualizar la posición objetivo

    public bool isFacingRight = false;
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private float updateTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;
        DontDestroyOnLoad(gameObject);

        // Inicializar la primera posición objetivo
        if (player != null)
        {
            targetPosition = player.position;
        }
        updateTimer = updateInterval;
    }

    private void FixedUpdate()
    {
        UpdateTargetPosition();
        MoveToTarget();

        bool isTargetRight = transform.position.x < targetPosition.x;
        Flip(isTargetRight);
    }

    private void UpdateTargetPosition()
    {
        updateTimer -= Time.fixedDeltaTime;

        if (updateTimer <= 0f && player != null)
        {
            targetPosition = player.position;
            updateTimer = updateInterval;
        }
    }

    private void MoveToTarget()
    {
        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    public void Attack()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Die();
            Debug.Log("Cocodrilo atacó al jugador y le quitó una vida");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            Attack();
        }
    }

    private void Flip(bool isTargetRight)
    {
        if ((isTargetRight && !isFacingRight) || (!isTargetRight && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}