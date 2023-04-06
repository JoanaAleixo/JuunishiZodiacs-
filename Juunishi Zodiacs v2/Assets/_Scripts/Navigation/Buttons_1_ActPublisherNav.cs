using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_1_ActPublisherNav : MonoBehaviour
{
   public void Book()
    {
         SceneManager.LoadScene("Nav_1_ActPublisher", LoadSceneMode.Single);
    }
    
    public void GoingDown()
    {
         SceneManager.LoadScene("N_1_ActPublisherNavP2", LoadSceneMode.Single);
    }
    
    public void Elevator()
    {
         SceneManager.LoadScene("Nav_1_ActElevator", LoadSceneMode.Single);
    }
    
    public void Street()
    {
         SceneManager.LoadScene("N_1_ActPublisherStreetFireNav", LoadSceneMode.Single);
    }
}
