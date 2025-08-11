using UnityEngine;
using System.Collections.Generic;

public class AStarPathfinding : MonoBehaviour
{
    [Header("Configuración")]
    public Jugador targetPlayer; // Referencia al script del jugador
    public LayerMask obstacleMask;
    public float gridSize = 10f; // Debe coincidir con cellSize del jugador
    public float moveSpeed = 50f;
    public float recalculateRate = 0.5f; // Tasa de recálculo del camino

    private List<Node> path;
    private int targetIndex;
    private float lastRecalculationTime;

    void Start()
    {
        // Alinear inicialmente a la cuadrícula
        SnapToGrid();
        lastRecalculationTime = Time.time;
    }

    void Update()
    {
        // Recalcular camino periódicamente o cuando el objetivo se mueva
        if (Time.time - lastRecalculationTime > recalculateRate ||
            (path != null && path.Count > 0 && Vector3.Distance(path[path.Count - 1].position, targetPlayer.transform.position) > gridSize))
        {
            FindPath(transform.position, targetPlayer.transform.position);
            lastRecalculationTime = Time.time;
        }

        if (path != null && path.Count > 0)
        {
            MoveAlongPath();
        }
    }

    void SnapToGrid()
    {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize,
            Mathf.Round(transform.position.y / gridSize) * gridSize,
            Mathf.Round(transform.position.z / gridSize) * gridSize
        );
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // Alinear posiciones a la cuadrícula
        startPos = new Vector3(
            Mathf.Round(startPos.x / gridSize) * gridSize,
            Mathf.Round(startPos.y / gridSize) * gridSize,
            Mathf.Round(startPos.z / gridSize) * gridSize
        );

        targetPos = new Vector3(
            Mathf.Round(targetPos.x / gridSize) * gridSize,
            Mathf.Round(targetPos.y / gridSize) * gridSize,
            Mathf.Round(targetPos.z / gridSize) * gridSize
        );

        Node startNode = new Node(startPos);
        Node targetNode = new Node(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost ||
                    (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.position == targetNode.position)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!IsWalkable(neighbor.position) || closedSet.Contains(neighbor))
                    continue;

                float newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        targetIndex = 0;
    }

    void MoveAlongPath()
    {
        if (targetIndex >= path.Count)
        {
            path = null;
            return;
        }

        Vector3 targetPosition = path[targetIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetIndex++;
        }
    }

    List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // Solo movimientos ortogonales (como el personaje perseguido)
        Vector3[] directions = {
            Vector3.forward * gridSize,
            Vector3.back * gridSize,
            Vector3.right * gridSize,
            Vector3.left * gridSize
        };

        foreach (Vector3 dir in directions)
        {
            Vector3 neighborPos = node.position + dir;
            neighbors.Add(new Node(neighborPos));
        }

        return neighbors;
    }

    bool IsWalkable(Vector3 position)
    {
        return !Physics.CheckSphere(position, gridSize / 2, obstacleMask);
    }

    float GetDistance(Node nodeA, Node nodeB)
    {
        // Distancia Manhattan (adecuada para movimientos ortogonales)
        return Mathf.Abs(nodeA.position.x - nodeB.position.x) +
               Mathf.Abs(nodeA.position.y - nodeB.position.y) +
               Mathf.Abs(nodeA.position.z - nodeB.position.z);
    }
}

public class Node
{
    public Vector3 position;
    public Node parent;
    public float gCost;
    public float hCost;
    public float fCost { get { return gCost + hCost; } }

    public Node(Vector3 position)
    {
        this.position = position;
    }
}

