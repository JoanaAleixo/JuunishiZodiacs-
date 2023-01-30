using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    LoadingSceneManager sceneInstance;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject extrasMenu;

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
        sceneInstance.LoadScene(1);
    }

    public void CombatSceneButton()
    {
        sceneInstance.LoadCombatScene(EnemyPreTest);
    }

    public void OptionsMenuButton()
    {
        optionMenu.gameObject.SetActive(true);
    }

    public void ExtrasButton()
    {
        extrasMenu.gameObject.SetActive(true);
    }

    public void BackButton()
    {
        optionMenu.gameObject.SetActive(false);
        extrasMenu.gameObject.SetActive(false);
    }

    public void ExitButton()
    {
        Debug.Log("Sair");
        Application.Quit();
    }
    #endregion
}
