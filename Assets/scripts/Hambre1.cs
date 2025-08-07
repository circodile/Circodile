using UnityEngine;

public class Hambre1 : MonoBehaviour
{
    [Header("Hambre")]
    public float hambre = 100f;
    public float velocidadBajada = 5f; // unidades por minuto

    public Comida comidaCercana;

    void Update()
    {
        // Bajar hambre con el tiempo
        hambre -= velocidadBajada * Time.deltaTime / 60f;
        hambre = Mathf.Clamp(hambre, 0, 100);

        // Comer si hay comida cerca y se presiona E
        if (comidaCercana != null && Input.GetKeyDown(KeyCode.E))
        {
            Comer(comidaCercana.valorNutricional);
            Destroy(comidaCercana.gameObject);
            comidaCercana = null;
        }
    }

    public void Comer(float cantidad)
    {
        hambre += cantidad;
        hambre = Mathf.Clamp(hambre, 0, 100);
        Debug.Log("Comiste. Hambre actual: " + hambre);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            comidaCercana = other.GetComponent<Comida>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            comidaCercana = null;
        }
    }
}
