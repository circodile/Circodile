using UnityEngine;
using TMPro;
using System.Collections;

public class MensajeJuego : MonoBehaviour
{
    [Header("Referencia al texto UI")]
    public TextMeshProUGUI mensajeTexto;

    [Header("Tiempo que aparece el mensaje inicial")]
    public float duracionMensajeInicial = 5f;

    private bool cercaDelBarco = false;

    void Awake()
    {
        // Buscar el TextMeshPro automáticamente si no lo asignaste
        if (mensajeTexto == null)
            mensajeTexto = GameObject.Find("MensajeUI").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        // Mostrar mensaje inicial al comenzar el juego
        mensajeTexto.text = "Recolecta materiales para construir el barco";
        StartCoroutine(OcultarMensajeInicial());
    }

    private void Update()
    {
        // Cambiar mensaje si está cerca del barco
        if (cercaDelBarco)
        {
            mensajeTexto.text = "Se necesita 10 de MADERA, 5 de TELA y 5 de CINTA";
        }
    }

    // Coroutine para ocultar el mensaje inicial después de unos segundos
    private IEnumerator OcultarMensajeInicial()
    {
        yield return new WaitForSeconds(duracionMensajeInicial);
        if (!cercaDelBarco)
            mensajeTexto.text = "";
    }

    // Detectar proximidad al barco
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barco"))
        {
            cercaDelBarco = true;
            mensajeTexto.text = "Se necesita 10 de MADERA, 5 de TELA y 5 de CINTA";
            Debug.Log("Jugador cerca del barco");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Barco"))
        {
            cercaDelBarco = false;
            mensajeTexto.text = ""; // desaparecer mensaje
            Debug.Log("Jugador se alejó del barco");
        }
    }
}
