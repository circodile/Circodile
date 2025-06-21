using UnityEngine;

public class Alimentos : MonoBehaviour
{
    public static Alimentos Instancia { get; private set; }
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
            Debug.Log($"Comiste. Hambre actual: {hambre}");
        }

        public override void Beber(float cantidad) { } // No aplica
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
            Debug.Log($"Bebiste. Sed actual: {sed}, Mate: {mateActual}");
        }

        public override void Comer(float cantidad) { } // No aplica
    }

    // ---------------------- VARIABLES INTERNAS ----------------------
    private Hambre sistemaHambre;
    private Sed sistemaSed;
    private Comida comidaCercana;
    private Bebida bebidaCercana;

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
        DontDestroyOnLoad(gameObject); // Persiste entre escenas

        sistemaHambre = new Hambre();
        sistemaSed = new Sed();
    }

    private void Start()
    {
        sistemaHambre = new Hambre();
        sistemaSed = new Sed();

        ultimoTiempo = Time.time;
    }

    private void Update()
    {
        float delta = Time.time - ultimoTiempo;
        ultimoTiempo = Time.time;

        // Bajar hambre y sed con el tiempo
        sistemaHambre.hambre -= sistemaHambre.velBajada * delta / 60f;
        Debug.Log($"Hambre actual: {sistemaHambre.hambre}");
        sistemaHambre.hambre = Mathf.Clamp(sistemaHambre.hambre, 0, 100);

        sistemaSed.sed -= sistemaSed.velBajada * delta / 60f;
        sistemaSed.sed = Mathf.Clamp(sistemaSed.sed, 0, 100);
        Debug.Log($"sed actual: {sistemaSed.sed}");

        // Comer
        if (Input.GetKeyDown(KeyCode.E) && comidaCercana != null)
        {
            sistemaHambre.Comer(comidaCercana.valorNutricional);
            Destroy(comidaCercana.gameObject);
            comidaCercana = null;
            Debug.Log("Comiste comida.");
        }

        // Beber
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (sistemaSed.mateActual > 0)
            {
                sistemaSed.Beber(20f);
            }
            else
            {
                Debug.Log("¡No hay agua en el mate!");
            }
        }

        // Recargar mate
        if (Input.GetKeyDown(KeyCode.E) && bebidaCercana != null)
        {
            sistemaSed.mateActual = sistemaSed.mateMaximo;
            Debug.Log("Recargaste el mate.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            comidaCercana = other.GetComponent<Comida>();
        }
        else if (other.CompareTag("Agua"))
        {
            bebidaCercana = other.GetComponent<Bebida>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            comidaCercana = null;
        }
        else if (other.CompareTag("Agua"))
        {
            bebidaCercana = null;
        }
    }
}


/*
 cod cafa
using UnityEngine;

public class Sed : MonoBehaviour
{
    public float sedMaxima = 10f;
    public float sedActual = 10f;
    public float velocidadDeBajada = 0.01f;

    public float mateMaximo = 3f;
    public float mateActual = 3f;


    void Update()
    {
        // Baja sed con el tiempo
        sedActual -= velocidadDeBajada * Time.deltaTime;

        // Limitar entre 0 y sedMaxima
        sedActual = Mathf.Clamp(sedActual, 0f, sedMaxima);

        // Ejemplo: si la sed es 0, hacemos algo
        if (sedActual <= 0f)
        {
            Debug.Log("¡Estás deshidratado!");
            // Aquí podrías empezar a quitar vida, mover más lento, etc.
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mateActual > 0f)
            {

                sedActual = 10f; // Recupera 20 de sed
                Debug.Log("Tomaste agua");
                mateActual -= 1;
                Debug.Log(mateActual);
            }
            else
            {
                Debug.Log("no hay mas agua");
            }
        }

    }

}
*/