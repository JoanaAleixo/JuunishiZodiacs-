using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial;

    public void Botao()
    {
        tutorial.gameObject.SetActive(false);
    }
}
