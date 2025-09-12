using System.Collections;
using UnityEngine;
using TMPro;

public class Alimentos : MonoBehaviour
{
    public static Alimentos Instancia { get; private set; }

    [Header("UI Mensajes")]
    public TextMeshProUGUI mensajeTexto;
    public float mensajeDuracion = 5f;  

    private Coroutine mensajeCoroutine;
    private string mensajesAcumulados = "";

    [Header("Audio")]
    public AudioClip sonidoComida;
    private AudioSource audioSource;

    // ---------------------- ABSTRACTA ----------------------
    public abstract class AAlimento
    {
        public float hambre;
        public float sed;
        public float velBajada;

        public abstract void Comer(float cantidad);
        public abstract void Beber(float cantidad);
    }

    // ---------------------- HAMBRE ----------------------
    public class Hambre : AAlimento
    {
        public Hambre()
        {
            hambre = 100f;
            velBajada = 20f;
        }

        public override void Comer(float cantidad)
        {
            hambre += cantidad;
            hambre = Mathf.Clamp(hambre, 0, 100);
            Instancia.MostrarMensajeTemporal($"Comiste. Hambre actual: {hambre}");
        }

        public override void Beber(float cantidad) { }
    }

    // ---------------------- SED ----------------------
    public class Sed : AAlimento
    {
        public float mateMaximo = 3f;
        public float mateActual = 3f;

        public Sed()
        {
            sed = 100f;
            velBajada = 5f;
        }

        public override void Beber(float cantidad)
        {
            sed += cantidad;
            sed = Mathf.Clamp(sed, 0, 100);
            mateActual--;
            Instancia.MostrarMensajeTemporal($"Bebiste. Sed actual: {sed}, Mate restante: {mateActual}");
        }

        public override void Comer(float cantidad) { }
    }

    // ---------------------- VARIABLES INTERNAS ----------------------
    public Hambre sistemaHambre;
    public Sed sistemaSed;

    private bool cercaDeComida = false;
    private bool cercaDeAgua = false;

    private Comida comidaCercana = null;
    private Bebida bebidaCercana = null;

    private float ultimoTiempo;

    // ---------------------- UNITY ----------------------
    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);

        sistemaHambre = new Hambre();
        sistemaSed = new Sed();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        ultimoTiempo = Time.time;
    }

    private void Update()
    {
        float delta = Time.time - ultimoTiempo;
        ultimoTiempo = Time.time;

        // Reducir hambre y sed con el tiempo
        sistemaHambre.hambre -= sistemaHambre.velBajada * delta / 60f;
        sistemaHambre.hambre = Mathf.Clamp(sistemaHambre.hambre, 0, 100);

        sistemaSed.sed -= sistemaSed.velBajada * delta / 60f;
        sistemaSed.sed = Mathf.Clamp(sistemaSed.sed, 0, 100);

        // ---------------------- ACCIONES ----------------------
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cercaDeComida && comidaCercana != null)
            {
                sistemaHambre.Comer(comidaCercana.valorNutricional);

                // Reproducir sonido de comida
                if (sonidoComida != null)
                    audioSource.PlayOneShot(sonidoComida);

                Destroy(comidaCercana.gameObject);
                comidaCercana = null;
                cercaDeComida = false;
            }
            else if (cercaDeAgua)
            {
                sistemaSed.mateActual = sistemaSed.mateMaximo;
                MostrarMensajeTemporal("Recargaste el mate.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (sistemaSed.mateActual > 0)
            {
                sistemaSed.Beber(20f);
            }
            else
            {
                MostrarMensajeTemporal("¡No hay agua en el mate!");
            }
        }
    }

    // ---------------------- DETECCIÓN DE TRIGGERS ----------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            comidaCercana = other.GetComponent<Comida>();
            cercaDeComida = true;
        }
        else if (other.CompareTag("Agua"))
        {
            cercaDeAgua = true;
            bebidaCercana = other.GetComponent<Bebida>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            comidaCercana = null;
            cercaDeComida = false;
        }
        else if (other.CompareTag("Agua"))
        {
            cercaDeAgua = false;
            bebidaCercana = null;
        }
    }

    // ---------------------- COROUTINE PARA MENSAJES ----------------------
    private void MostrarMensajeTemporal(string texto)
    {
        mensajesAcumulados += texto + "\n";

        if (mensajeCoroutine != null)
            StopCoroutine(mensajeCoroutine);

        mensajeCoroutine = StartCoroutine(MostrarMensajeCoroutine());
    }

    private IEnumerator MostrarMensajeCoroutine()
    {
        mensajeTexto.text = mensajesAcumulados;
        yield return new WaitForSeconds(mensajeDuracion);
        mensajesAcumulados = "";
        mensajeTexto.text = "";
        mensajeCoroutine = null;
    }
}
