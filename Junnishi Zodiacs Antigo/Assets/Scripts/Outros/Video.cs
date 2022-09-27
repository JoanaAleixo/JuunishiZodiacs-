using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video : MonoBehaviour
{
    [SerializeField] GameObject objetoAtivar;
    [SerializeField] float temporizador;
    

    void Update()
    {
        temporizador -= Time.deltaTime;
        if(temporizador <= 0)
        {
            objetoAtivar.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
