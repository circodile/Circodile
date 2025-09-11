using UnityEngine;

public class SonidoFuego : MonoBehaviour
{
    [Header("Sonido al chocar con fuego")]
    public AudioClip sonidoFuego;   // el clip que querés reproducir
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fuego") || collision.gameObject.CompareTag("Enemigo"))
        {
            audioSource.PlayOneShot(sonidoFuego);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fuego") || other.CompareTag("Enemigo"))
        {
            audioSource.PlayOneShot(sonidoFuego);
        }
    }
}
