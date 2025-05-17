using UnityEngine;

public class movement_player : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float cellSize = 10f; // Tamaño de cada casilla
    [SerializeField] private float moveSpeed = 50f; // Velocidad de movimiento
    [SerializeField] private LayerMask obstacleLayer; // Capa de obstáculos

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Vector3 facingDirection = Vector3.right; // Dirección inicial

    void Start()
    {
        // Alinear inicialmente a la cuadrícula
        targetPosition = new Vector3(
            Mathf.Round(transform.position.x / cellSize) * cellSize,
            Mathf.Round(transform.position.y / cellSize) * cellSize,
            Mathf.Round(transform.position.z / cellSize) * cellSize
        );
        transform.position = targetPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
            return;
        }

        // Capturar entrada
        Vector3 inputDirection = GetInputDirection();

        if (inputDirection != Vector3.zero)
        {
            TryMove(inputDirection);
        }
    }

    Vector3 GetInputDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            return Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            return Vector3.back;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            return Vector3.left;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            return Vector3.right;

        return Vector3.zero;
    }

    void TryMove(Vector3 direction)
    {
        facingDirection = direction; // Actualizar dirección actual
        Vector3 newPosition = targetPosition + direction * cellSize;

        // Verificar obstáculos
        if (!Physics.CheckSphere(newPosition, 150f, obstacleLayer))
        {
            targetPosition = newPosition;
            isMoving = true;
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Comprobar si llegó al destino
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
