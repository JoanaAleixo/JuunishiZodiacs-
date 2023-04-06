using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_1_ActNearRiverNav : MonoBehaviour
{
    public void NearTemple()
    {
         SceneManager.LoadScene("N_1_ActNearTempleNav", LoadSceneMode.Single);
    }
    
    public void Back()
    {
         SceneManager.LoadScene("N_1_ActTrainStationNav", LoadSceneMode.Single);

    } 
}
