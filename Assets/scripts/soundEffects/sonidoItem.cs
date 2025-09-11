using UnityEngine;

public class SonidoItem : MonoBehaviour
{
    [Header("Sonido al chocar con Item")]
    public AudioClip sonidoItem;   // el clip que querés reproducir
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            audioSource.PlayOneShot(sonidoItem);
        }
    }
}
