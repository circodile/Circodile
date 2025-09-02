using UnityEngine;
using UnityEngine.UI;

public class vidas_ui : MonoBehaviour
{
    public vidas_jugador viditas;
    public int vidas;
    public Image vida1;
    public Image vida2;
    public Image vida3;
    public Sprite corazonLleno;
    public Sprite corazonVacio;

    void Update()
    {
        vidas = viditas.vida;
        update_vidas();
    }

    public void update_vidas() 
    {
        if (vidas == 3)
        {
            vida3.sprite = corazonLleno;
            vida2.sprite = corazonLleno;
            vida1.sprite = corazonLleno;
        } else if (vidas == 2)
        {
            vida3.sprite = corazonVacio;
            vida2.sprite = corazonLleno;
            vida1.sprite = corazonLleno;
        } else if (vidas == 1)
        {
            vida3.sprite = corazonVacio;
            vida2.sprite = corazonVacio;
            vida1.sprite = corazonLleno;
        } else if (vidas == 0)
        {
            vida3.sprite = corazonVacio;
            vida2.sprite = corazonVacio;
            vida1.sprite = corazonVacio;
        }
    }

}

