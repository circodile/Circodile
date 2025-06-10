using UnityEngine;

public class Alimentos : MonoBehaviour
{
    public abstract class AAlimento
    {
        public float hambre;
        public float sed;
        public float velBajada;

        public abstract void Comer(float cantidad);
        public abstract void Beber(float cantidad);
    }

    public class Hambre : AAlimento
    {
        public Hambre()
        {
            hambre = 100f;
            velBajada = 5f;
        }

        private Comida comidaCercana;

        public override void Comer(float cantidad)
        {
            hambre -= cantidad; 
            hambre = Mathf.Clamp(hambre, 0, 100);
            Debug.Log($"Comiste. Hambre actual: {hambre}");
        }

        public override void Beber(float cantidad) { } // Implementación vacía (no la uso)
    }

    public class Sed : AAlimento
    {
        public float mateMaximo = 3f;
        public float mateActual = 3f;

        public Sed()
        {
            sed = 100f;
            velBajada = 5f;
        }

        private Bebida bebidaCercana;

        public override void Beber(float cantidad)
        {
            sed -= cantidad;
            sed = Mathf.Clamp(sed, 0, 100);
            mateActual--;
            Debug.Log($"Bebiste. Sed actual: {sed}, Mate: {mateActual}");
        }

       public void cargarMate()
        {
            if (collider.gameObject.CompareTag("agua") && Input.GetKeyDown(KeyCode.Q))
            {
                mateActual = 3f;
            }
        }

        public override void Comer(float cantidad) { } // Implementación vacía (no aplica)
    }

    // Instancias 
    private Hambre sistemaHambre;
    private Sed sistemaSed;
    private Comida comidaCercana;
    private Bebida bebidaCercana;

    private void Start()
    {
        sistemaHambre = new SistemaHambre();
        sistemaSed = new SistemaSed();
    }

    private void Update()
    {
        // Reducir hambre y sed con el tiempo
        sistemaHambre.hambre -= sistemaHambre.velBajada * Time.deltaTime / 60f;
        sistemaHambre.hambre = Mathf.Clamp(sistemaHambre.hambre, 0, 100);

        sistemaSed.sed -= sistemaSed.velBajada * Time.deltaTime / 60f;
        sistemaSed.sed = Mathf.Clamp(sistemaSed.sed, 0, 100);

        // Comer (E) o Beber (Q)
        if (Input.GetKeyDown(KeyCode.E) && comidaCercana != null)
        {
            sistemaHambre.Comer(comidaCercana.valorNutricional);
            Destroy(comidaCercana.gameObject);
            comidaCercana = null;
        }

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

        if(aguaCercana != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                mateActual = 3f;
            }
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