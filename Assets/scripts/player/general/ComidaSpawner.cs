using UnityEngine;

public class ComidaSpawner : MonoBehaviour
{
    public GameObject[] comidaPrefabs;
    public float tiempoEntreSpawns = 10f;
    public Transform[] puntosSpawn;

    void Start()
    {
        InvokeRepeating(nameof(SpawnComida), 2f, tiempoEntreSpawns);
    }

    void SpawnComida()
    {
        if (comidaPrefabs.Length == 0 || puntosSpawn.Length == 0) return;

        int index = Random.Range(0, comidaPrefabs.Length);
        int punto = Random.Range(0, puntosSpawn.Length);

        if (comidaPrefabs[index] != null && puntosSpawn[punto] != null)
        {
            Instantiate(comidaPrefabs[index], puntosSpawn[punto].position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab o punto de spawn es null");
        }
    }
}
