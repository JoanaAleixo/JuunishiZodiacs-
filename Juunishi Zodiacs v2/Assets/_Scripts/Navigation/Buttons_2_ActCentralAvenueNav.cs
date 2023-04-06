using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_2_ActCentralAvenueNav : MonoBehaviour
{
     public void BackAlley()
    {
         SceneManager.LoadScene("N_2_ActBackAlleyNav", LoadSceneMode.Single);
    } 
    
    public void StationStreet()
    {
         SceneManager.LoadScene("N_2_ActTrainStationNav", LoadSceneMode.Single);
    } 
    
    public void Caffe()
    {
         SceneManager.LoadScene("Nav_2_ActCafe", LoadSceneMode.Single);
    }
}
