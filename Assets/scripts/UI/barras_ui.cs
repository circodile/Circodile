using UnityEngine;
using UnityEngine.UI;

public class Barras_ui : MonoBehaviour
{
    public Slider Hambre;
    public Slider Sed;
    public Alimentos alimentos;

    void Awake()
    {
        if (alimentos == null) alimentos = Alimentos.Instancia ?? FindObjectOfType<Alimentos>();
    }

    void Start()
    {
        Sed.minValue = 0; 
        Sed.maxValue = 100;
        Hambre.minValue = 0; 
        Hambre.maxValue = 100;
    }

    void Update()
    {
        if (!alimentos) return;
        Sed.value = alimentos.sistemaSed.sed;
        Hambre.value = alimentos.sistemaHambre.hambre;
    }
}
