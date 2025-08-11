using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadNormal = 5f;
    public float velocidadBaja = 2f;

    public float velocidadActual;
    public Rigidbody2D rb;
   // private Alimentos hambreScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //   hambreScript = GetComponent<Hambre1>();
        velocidadActual = velocidadNormal;
    }

    void FixedUpdate()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Vector2 movimiento = new Vector2(movX, movY).normalized;
        rb.linearVelocity = movimiento * velocidadActual;
    }
}
