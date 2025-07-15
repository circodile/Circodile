using UnityEngine;

public class CrocodileManager : MonoBehaviour
{
    public enum State { Patrolling, Chasing }

    [Header("Crocodile Settings")]
    public State currentState;
    public Transform player;
    public float speed;
    public float detectionRange;
    public bool seesPlayer;

    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    [Header("Chase Settings")]
    public float chaseSpeed = 5f;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float attackDamage = 10f;

    void Start()
    {
        currentState = State.Patrolling;
    }

    void Update()
    {
        CheckLineOfSight();

        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                ChasePlayer();
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

        // Mirar hacia el punto de patrullaje
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Cambiar al siguiente punto cuando llegue
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        if (currentState == State.Chasing && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Si está suficientemente cerca, atacar
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                // Perseguir al jugador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

                // Mirar hacia el jugador
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }
    }

    void AttackPlayer()
    {
        // Aquí puedes agregar animación de ataque
        Debug.Log("¡Cocodrilo ataca al jugador!");

        // Quitar vida al jugador
        vidas_jugador playerHealth = player.GetComponent<vidas_jugador>();
        if (playerHealth != null)
        {
            playerHealth.vidas--;

            // Verificar si el jugador murió
            if (playerHealth.vidas <= 0)
            {
                Destroy(player.gameObject);
            }
        }
    }

    void CheckLineOfSight()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificar si el jugador está en rango de detección
        if (distanceToPlayer <= detectionRange)
        {
            // Raycast para verificar line of sight
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
            {
                // Si el raycast golpea al jugador y no está muerto
                if (hit.transform == player)
                {
                    vidas_jugador playerHealth = player.GetComponent<vidas_jugador>();
                    bool playerIsAlive = playerHealth != null && playerHealth.vidas > 0;

                    if (playerIsAlive)
                    {
                        seesPlayer = true;
                        currentState = State.Chasing;
                    }
                    else
                    {
                        seesPlayer = false;
                        currentState = State.Patrolling;
                    }
                }
                else
                {
                    seesPlayer = false;
                    currentState = State.Patrolling;
                }
            }
        }
        else
        {
            seesPlayer = false;
            currentState = State.Patrolling;
        }
    }


}