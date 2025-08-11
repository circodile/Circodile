using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CrocodileManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float speed = 2f;

    public bool isFacingRight = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;

        DontDestroyOnLoad(gameObject); // El cocodrilo sigue persiguiendo entre escenas
    }


    private void FixedUpdate()
    {
        Follow();

        bool isPlayerRight = transform.position.x < player.position.x;
        Flip(isPlayerRight);
    }

    private void Follow()
    {
        if (Vector2.Distance(transform.position, player.position) > minDistance)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, player.position, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
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

    private void Flip(bool isPlayerRight)
    {
        if ((isPlayerRight && !isFacingRight) || (!isPlayerRight && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
