using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //melhor GameManager do ano, brilha meu filho 
    
    bool _pausedGame;


    static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }
    public bool PausedGame { get => _pausedGame; set => _pausedGame = value; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {

    }

    #region Paused Menu
    public void Pause()
    {
        _pausedGame = true;
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        _pausedGame = false;
        Time.timeScale = 1;
    }
    #endregion

}
