using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_2_ActTrainStationNav : MonoBehaviour
{
     public void Station()
    {
         SceneManager.LoadScene("Nav_2_ActTrainStation", LoadSceneMode.Single);
    }
    
    public void NearRiver()
    {
         SceneManager.LoadScene("N_2_ActNearRiverNav", LoadSceneMode.Single);
    }
    
    public void PublisherStreet()
    {
         SceneManager.LoadScene("N_2_ActPublisherStreetNav", LoadSceneMode.Single);
    }
}
