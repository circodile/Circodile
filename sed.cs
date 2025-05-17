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

    public void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("agua") && Input.GetKeyDown(KeyCode.Q))
        {
            mateActual = 3f;
        }
    }

}

