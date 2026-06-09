using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Image fullscreenButton;

    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    [SerializeField] PlayerInput playerInput;

    void Start()
    {
        resolutions = Screen.resolutions;
        if (resolutionDropdown != null )
        {
            resolutionDropdown.ClearOptions();
            int currentResI = 0;
            List<string> options = new List<string>();
            for (int i = 0; i < resolutions.Length; i++)
            {
                options.Add($"{resolutions[i].width} x {resolutions[i].height}");

                if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResI = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResI;
            resolutionDropdown.RefreshShownValue();
        }
    }

    // MAIN MENU
    public void PlayAction()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGameAction()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void QuitAction()
    {
        Application.Quit();
    }
    public void OpenOptions()
    {
        if (mainCanvas != null)
        {
            mainCanvas.SetActive(false);
        }
        else
        {
            pauseCanvas.SetActive(false);
        }
        optionsCanvas.SetActive(true);
        
    }

    // OPTIONS
    public void CloseOptions()
    {
        if (mainCanvas != null)
        {
            mainCanvas.SetActive(true);
        }
        else
        {
            pauseCanvas.SetActive(true);
        }
        optionsCanvas.SetActive(false);
    }
    public void ResumeAction()
    {
        playerInput.actions.FindActionMap("Player").Enable();
        playerInput.actions.FindActionMap("Options").Disable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }
    public void ChangeVolume(float vol)
    {
        audioMixer.SetFloat("MasterVolume", vol);
    }
    public void ChangeSensitivity(float sens)
    {
        Settings.mouseSensitivity = sens;
    }
    public void ChangeTextSpeed(float speed)
    {
        Settings.textSpeed = speed;
    }
    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
            fullscreenButton.color = new Color(1, 1, 1, 0);
        }
        else
        {
            Screen.fullScreen = true;
            fullscreenButton.color = new Color(1, 1, 1, 1);
        }
    }
    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
