using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);

        AudioListener.volume = volumeSlider.value;
        SetMouseSensitivity(sensitivitySlider.value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseOptions()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().ResumeGame();
        }
    }

    public void OnVolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void OnSensitivityChange()
    {
        SetMouseSensitivity(sensitivitySlider.value);
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivitySlider.value);
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().mouseSensitivity = sensitivity;
        }
    }
}
