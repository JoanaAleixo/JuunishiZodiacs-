using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Buttons_1_ActNearTempleNav : MonoBehaviour
{
    public void Back()
    {
         SceneManager.LoadScene("N_1_ActNearRiverNav", LoadSceneMode.Single);
    }
    
    public void Temple()
    {
         SceneManager.LoadScene("Nav_1_ActNearTemple", LoadSceneMode.Single);
    } 
}
