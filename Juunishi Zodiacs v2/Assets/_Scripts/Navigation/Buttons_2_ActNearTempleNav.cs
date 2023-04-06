using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_2_ActNearTempleNav : MonoBehaviour
{
    public void Temple()
    {
         SceneManager.LoadScene("Nav_2_ActTemple", LoadSceneMode.Single);
    }
    
    public void TempleBack()
    {
         SceneManager.LoadScene("N_2_ActNearTempleNav", LoadSceneMode.Single);
    }

    public void NearRiver()
    {
         SceneManager.LoadScene("N_2_ActNearRiverNav", LoadSceneMode.Single);
    }
}
