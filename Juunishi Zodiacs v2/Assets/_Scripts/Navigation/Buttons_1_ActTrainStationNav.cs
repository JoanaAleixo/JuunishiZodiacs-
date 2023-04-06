using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_1_ActTrainStationNav : MonoBehaviour
{
   public void TrainStation()
    {
         SceneManager.LoadScene("Nav_1_ActTrainStation", LoadSceneMode.Single);
    } 
    
    public void NearRiver()
    {
         SceneManager.LoadScene("Nav_1_ActNearRiver", LoadSceneMode.Single);
    }
    
    public void Back()
    {
         SceneManager.LoadScene("N_1_ActPublisherStreetNav", LoadSceneMode.Single);
    }
}
