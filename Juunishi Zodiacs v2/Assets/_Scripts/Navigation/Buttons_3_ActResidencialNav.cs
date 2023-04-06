using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_3_ActResidencialNav : MonoBehaviour
{
    public void PublisherStreet()
    {
         SceneManager.LoadScene("N_3_ActPublisherStreetNav", LoadSceneMode.Single);
    }
    
    public void AkiraHouse()
    {
         SceneManager.LoadScene("D_1_Act3.2", LoadSceneMode.Single);
    }
}
