using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

enum EstadoDoJogo { Diálogo, NaoDialogo, MenuPrincipal}


public class GameManager : MonoBehaviour
{
    //melhor GameManager do ano, brilha meu filho 

    [SerializeField] EstadoDoJogo estadoDoJogo;
    
    bool jogoPausado;

    internal EstadoDoJogo EstadoDoJogo { get => estadoDoJogo; set => estadoDoJogo = value; }

    static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }
    public bool JogoPausado { get => jogoPausado; set => jogoPausado = value; }

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


    public void Pausa()
    {
        jogoPausado = true;
        Time.timeScale = 0;
    }

    public void RetomarJogo()
    {
        jogoPausado = false;
        Time.timeScale = 1;
    }

}
