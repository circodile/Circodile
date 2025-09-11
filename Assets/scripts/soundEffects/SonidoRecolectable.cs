using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SonidoRecolectable : MonoBehaviour
{
    public float distanciaActivacion = 5f;   // rango en el que el jugador empieza a escuchar
    private Transform jugador;
    private AudioSource audioSource;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();

        // Configuración recomendada para audio espacial
        audioSource.playOnAwake = false;
        audioSource.loop = true;  // para que siempre suene mientras el jugador esté cerca
        audioSource.spatialBlend = 1f; // 1 = totalmente 3D, 0 = 2D
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.minDistance = 1f;    // volumen máximo si está muy cerca
        audioSource.maxDistance = distanciaActivacion; // hasta dónde se escucha
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= distanciaActivacion)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}
