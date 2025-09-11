using UnityEngine;

public class SonidoComida : MonoBehaviour
{
    [Header("Sonido al comer comida")]
    public AudioClip sonidoComida;
    private AudioSource audioSource;
    private bool cercaDeComida = false;

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
        if (cercaDeComida && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(sonidoComida);
            Debug.Log("Comiste y sonó el audio.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            cercaDeComida = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            cercaDeComida = false;
        }
    }
}
