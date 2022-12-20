using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager instance;
    DialogUIManager ui;

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
        ui = GameObject.Find("UI Manager").GetComponent<DialogUIManager>();
    }


    void Update()
    {
        //ativar menu de pausa, só se pode aívá-lo durante diálogos

        //if(GameManager.Instance.EstadoDoJogo == EstadoDoJogo.Diálogo)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                if(ui == null)
                {
                    ui = GameObject.Find("UI Manager").GetComponent<DialogUIManager>();
                    
                }

                if (GameManager.Instance.PausedGame)
                {
                    GameManager.Instance.ContinueGame();
                  //  ui.MenuPausa.gameObject.SetActive(false);
                }
                else
                {
                    GameManager.Instance.Pause();
                   // ui.MenuPausa.gameObject.SetActive(true);
                }
            }
        }
    }
}
