using UnityEngine;

public class SonidoAgua : MonoBehaviour
{
    [Header("Sonido al beber agua")]
    public AudioClip sonidoAgua;
    private AudioSource audioSource;
    private bool cercaDeAgua = false;

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
        if (cercaDeAgua && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(sonidoAgua);
            Debug.Log("Bebiste agua y sonó el audio.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Agua"))
        {
            cercaDeAgua = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Agua"))
        {
            cercaDeAgua = false;
        }
    }
}
