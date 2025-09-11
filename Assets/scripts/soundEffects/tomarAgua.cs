using UnityEngine;

public class SonidoTeclaQ : MonoBehaviour
{
    [Header("Sonido al presionar Q")]
    public AudioClip sonidoQ;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Verificar si existe la instancia de Alimentos y hay agua en el mate
            if (Alimentos.Instancia != null && Alimentos.Instancia.sistemaSed.mateActual > 0)
            {
                audioSource.PlayOneShot(sonidoQ);
            }
            else
            {
                Debug.Log("No hay agua en el mate, no suena el audio.");
            }
        }
    }
}
