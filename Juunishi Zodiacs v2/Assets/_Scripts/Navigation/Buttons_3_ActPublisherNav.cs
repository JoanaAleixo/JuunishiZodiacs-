using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_3_ActPublisherNav : MonoBehaviour
{
    public void PublisherStreet()
    {
         SceneManager.LoadScene("N_3_ActPublisherStreetNav", LoadSceneMode.Single);
    }
    
    public void Elevator()
    {
         SceneManager.LoadScene("N_3_ActRoofTopNav", LoadSceneMode.Single);
    }
}
