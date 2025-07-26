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
    public Image[] corazones;            // Arreglo de imágenes UI para los corazones

    private void Start()
    {
        ActualizarCorazones();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Resta una vida al jugador
            vida--;
            ActualizarCorazones();

            if (vida <= 0)
            {
                Destroy(gameObject); // Destruye el objeto del jugador
            }
        }
    }

    void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            // Muestra corazón lleno si i es menor que la vida actual, sino muestra vacío
            if (i < vida)
            {
                corazones[i].sprite = corazonLleno;
            }
            else
            {
                corazones[i].sprite = corazonVacio;
            }

            // Activa/desactiva el corazón según si está dentro del rango de vidaTotal
            corazones[i].enabled = i < vidaTotal;
        }
    }

    public void PerderVida()
    {
        Console.WriteLine("El player perdio vida");
        vida--;
        if(vida <= 0)
        {
            SceneManager.LoadSceneAsync(4); 
        }
    }
}