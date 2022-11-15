using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager instance;
    UIManager ui;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    void Start()
    {
        ui = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }


    void Update()
    {
        //ativar menu de pausa, só se pode aívá-lo durante diálogos

        if(GameManager.Instance.EstadoDoJogo == EstadoDoJogo.Diálogo)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                if(ui == null)
                {
                    ui = GameObject.Find("UI Manager").GetComponent<UIManager>();
                    
                }

                if (GameManager.Instance.JogoPausado)
                {
                    GameManager.Instance.RetomarJogo();
                  //  ui.MenuPausa.gameObject.SetActive(false);
                }
                else
                {
                    GameManager.Instance.Pausa();
                   // ui.MenuPausa.gameObject.SetActive(true);
                }
            }
        }
    }
}
