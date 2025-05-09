using UnityEngine;

public class ComidaSpawner : MonoBehaviour
{
    public GameObject[] comidaPrefabs;
    public float tiempoEntreSpawns = 10f;
    public Transform[] puntosSpawn;

    void Start()
    {
        InvokeRepeating("SpawnComida", 2f, tiempoEntreSpawns);
    }

    void SpawnComida()
    {
        int index = Random.Range(0, comidaPrefabs.Length);
        int punto = Random.Range(0, puntosSpawn.Length);
        Instantiate(comidaPrefabs[index], puntosSpawn[punto].position, Quaternion.identity);
    }
}
