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
    public Image[] corazones;            // Arreglo de im�genes UI para los corazones

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Resta una vida al jugador
            vida--;
            Debug.Log("Vida del jugador: " + vida);

            if (vida <= 0)
            {
                Destroy(gameObject); // Destruye el objeto del jugador
            }
        }
    }


    public void PerderVida()
    {
        Console.WriteLine("El player perdio vida");
        vida--;
        if (vida <= 0)
        {
            SceneManager.LoadSceneAsync(4);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}


