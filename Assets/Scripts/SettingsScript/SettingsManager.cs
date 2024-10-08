using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider cursorSpeedSlider;
    public Toggle fullscreenToggle;

    private float mouseSensitivity = 1.0f;

    void Start()
    {
        // Set initial fullscreen and volume settings
        fullscreenToggle.isOn = Screen.fullScreen;
        volumeSlider.value = AudioListener.volume;

        // Set default or saved mouse speed value
        cursorSpeedSlider.value = PlayerPrefs.GetFloat("mouseSpeed", 1.0f);
        mouseSensitivity = cursorSpeedSlider.value;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMouseSpeed(float speed)
    {
        mouseSensitivity = speed;
        PlayerPrefs.SetFloat("mouseSpeed", speed);
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
