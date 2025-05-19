using UnityEngine;
using UnityEngine.UI;  // Para Slider, Dropdown
using TMPro;          // Si usas TextMeshPro

public class OptionsManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject optionsPanel; // Arrastra el panel aquí desde el editor.
    public Slider volumeSlider;
    public TMP_Dropdown qualityDropdown;

    void Start()
    {
        // Cargar valores guardados (si existen)
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.8f);
        qualityDropdown.value = PlayerPrefs.GetInt("Quality", 1); // 1 = Media

        // Configurar listeners (eventos)
        volumeSlider.onValueChanged.AddListener(SetVolume);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    // Método para abrir/cerrar el panel
    public void ToggleOptions(bool show)
    {
        optionsPanel.SetActive(show);
    }

    // Ajustar volumen
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume); // Guardar preferencia
    }

    // Ajustar calidad gráfica
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }
}