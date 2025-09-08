using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class audio: MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
       
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false; 
    }

    void OnBecameVisible()
    {
        //Debug.Log("Sprite 2D visible -> Play sonido");
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    void OnBecameInvisible()
    {
        //Debug.Log("Sprite 2D invisible -> Stop sonido");
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
