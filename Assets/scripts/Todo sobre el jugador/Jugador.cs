using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadNormal = 5f;
    public float velocidadBaja = 2f;
    public float velocidadActual;
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Animación")]
    public string parametroMovimiento = "Movement"; // Nombre del parámetro float (0 = idle, >0 = movimiento)
    public string parametroDireccion = "Direccion"; // Nombre del parámetro int (1=arriba, 2=abajo, 3=derecha, 4=izquierda)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidadActual = velocidadNormal;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Vector2 movimiento = new Vector2(movX, movY).normalized;
        rb.linearVelocity = movimiento * velocidadActual;

        ActualizarAnimacion(movX, movY);
    }

    void ActualizarAnimacion(float movX, float movY)
    {
        // 1. Controlar si hay movimiento (para el parámetro "Movement")
        bool estaMoviendose = (Mathf.Abs(movX) > 0.1f || Mathf.Abs(movY) > 0.1f);
        animator.SetFloat(parametroMovimiento, estaMoviendose ? 1f : 0f); // 1 = movimiento, 0 = idle

        // 2. Asignar dirección solo si hay movimiento
        if (estaMoviendose)
        {
            // Prioridad: Vertical (arriba/abajo) sobre horizontal (derecha/izquierda)
            if (movY > 0.1f) animator.SetInteger(parametroDireccion, 1); // Arriba
            else if (movY < -0.1f) animator.SetInteger(parametroDireccion, 2); // Abajo
            else if (movX > 0.1f) animator.SetInteger(parametroDireccion, 3); // Derecha
            else if (movX < -0.1f) animator.SetInteger(parametroDireccion, 4); // Izquierda
        }
        else
        {
            animator.SetInteger(parametroDireccion, 0); // Volver a idle
        }
    }
}