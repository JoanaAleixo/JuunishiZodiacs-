using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    LoadingSceneManager sceneInstance;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject creditssMenu;

    [SerializeField] GameObject EnemyPreTest;

    void Start()
    {
        sceneInstance = LoadingSceneManager.sceneInstance;
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
        sceneInstance.LoadScene("Title Screen",EnemyPreTest, true);
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
}
