using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_2_ActNearRiverNav : MonoBehaviour
{
    public void Temple()
    {
         SceneManager.LoadScene("N_2_ActNearTempleNav", LoadSceneMode.Single);
    }
    
    public void StationStreet()
    {
         SceneManager.LoadScene("N_2_ActTrainStationNav", LoadSceneMode.Single);
    }
}
