using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_2_ActResidencialNav : MonoBehaviour
{
   public void Fushimi()
    {
         SceneManager.LoadScene("D_1_Act2.1", LoadSceneMode.Single);
    }
    
    public void StationStreet()
    {
         SceneManager.LoadScene("N_2_ActPublisherStreetNav", LoadSceneMode.Single);
    }
}
