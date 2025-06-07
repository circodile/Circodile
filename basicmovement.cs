using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float moveSpeed = 5f; // Velocidad de movimiento

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene el Rigidbody2D autom�ticamente
    }

    void Update()
    {
        // Input del teclado (WASD o flechas)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Mueve al personaje (usando f�sica para evitar choques con colliders)
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}