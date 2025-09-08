using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class vidas_jugador : MonoBehaviour
{
    public int vidaTotal = 3;
    public int vida = 3;
    public Sprite corazonLleno;
    public Sprite corazonVacio;
    public Image[] corazones; // Arreglo de imágenes UI para los corazones
    private void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fuego"))
        {
            PerderVida();
            Debug.Log("¡El jugador ha sido quemado!");
        }
    }
    public void PerderVida()
    {
        Debug.Log("El player perdió vida");
        vida--;
        if (vida <= 0)
        {
            SceneManager.LoadSceneAsync(3);
        }
        UpdateHeartsUI();
    }
    private void UpdateHeartsUI()
    {
        if (corazones != null && corazones.Length > 0)
        {
            for (int i = 0; i < corazones.Length; i++)
            {
                if (i < vida)
                    corazones[i].sprite = corazonLleno;
                else
                    corazones[i].sprite = corazonVacio;
            }
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}