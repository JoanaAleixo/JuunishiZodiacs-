using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_2_ActPublisherStreetNav : MonoBehaviour
{
    public void Publisher()
    {
         SceneManager.LoadScene("Nav_2_ActPublisherStreet", LoadSceneMode.Single);
    }
    
    public void BackAlley()
    {
         SceneManager.LoadScene("N_2_ActBackAlleyNav", LoadSceneMode.Single);
    }
    
    public void StationStreet()
    {
         SceneManager.LoadScene("N_2_ActTrainStationNav", LoadSceneMode.Single);
    }
    
    public void Back()
    {
         SceneManager.LoadScene("N_2_ActResidencialNav", LoadSceneMode.Single);
    }
    
}
