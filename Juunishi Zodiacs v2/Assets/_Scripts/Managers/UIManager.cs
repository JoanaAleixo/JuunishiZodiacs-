using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    LoadingSceneManager sceneInstance;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject creditssMenu;

    [SerializeField] Slider sliderBrightness;
    [SerializeField] CanvasGroup canvasGroupBrightness;


    [SerializeField] GameObject enemyPreTest;
    [SerializeField] Sprite combatBackground;

    [SerializeField] GameObject _pauseMenu;

    void Start()
    {
        sceneInstance = LoadingSceneManager.sceneInstance;

        if (PlayerPrefs.HasKey("Bright"))
        {
            //Debug.Log("cacete");
            if(sliderBrightness != null)
            {
                sliderBrightness.value = PlayerPrefs.GetFloat("Bright");
                canvasGroupBrightness.alpha = sliderBrightness.value;
            }
            else
            canvasGroupBrightness.alpha = PlayerPrefs.GetFloat("Bright");

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CombatSceneButton();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //SoundManager.PlaySound(SoundManager.slashSound);
        }
    }

    #region Title Screen Buttons
    public void PressToStartButton()
    {
        titleScreen.gameObject.SetActive(true);
    }

    public void NewGameButton(string sceneName)
    {
        //SceneManager.LoadScene(sceneName);
        sceneInstance.LoadScene(sceneName);
    }

    public void CombatSceneButton()
    {
        sceneInstance.LoadScene("Title Screen",enemyPreTest, false, combatBackground);
    }

    public void OptionsMenuButton()
    {
        optionMenu.gameObject.SetActive(true);
    }

    public void ExtrasButton()
    {
        creditssMenu.gameObject.SetActive(true);
    }

    public void BackButton()
    {
        optionMenu.gameObject.SetActive(false);
        creditssMenu.gameObject.SetActive(false);
    }

    public void ExitButton()
    {
        Debug.Log("Sair");
        Application.Quit();
    }
    #endregion

    #region Sliders Volume e Brilho

    //public void VolumeSlider(float volume) //guardar os valores de volume
    //{
    //    audioMixer.SetFloat("Volume", volume);
    //    PlayerPrefs.SetFloat("Volume", volume);
    //    PlayerPrefs.Save(); //<- não esquecer 
    //}

    public void BrightnessSlider(float brightness) //guardar os valores de brilho
    {
        brightness = sliderBrightness.value;
        canvasGroupBrightness.alpha = sliderBrightness.value;
        PlayerPrefs.SetFloat("Bright", brightness);
        //Debug.Log("cacete2");
        PlayerPrefs.Save();
    }
    #endregion


    public void OpenClosePauseMenu()
    {
        if (_pauseMenu.activeSelf == true)
        {
            _pauseMenu.SetActive(false);
        }
        else
        {
            _pauseMenu.SetActive(true);
        }
    }
}
