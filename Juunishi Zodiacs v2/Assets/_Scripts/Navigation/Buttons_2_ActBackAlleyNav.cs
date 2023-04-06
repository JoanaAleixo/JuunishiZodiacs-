using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_2_ActBackAlleyNav : MonoBehaviour
{
    public void Back()
    {
         SceneManager.LoadScene("N_2_ActPublisherStreetNav", LoadSceneMode.Single);
    } 
    
    public void CentralAvenue()
    {
         SceneManager.LoadScene("Nav_2_ActCentralAvenue", LoadSceneMode.Single);
    } 
}
