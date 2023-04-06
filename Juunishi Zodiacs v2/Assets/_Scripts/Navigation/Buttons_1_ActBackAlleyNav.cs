using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_1_ActBackAlleyNav : MonoBehaviour
{
    // Start is called before the first frame update
    public void CentralAvenue()
    {
         SceneManager.LoadScene("Nav_1_ActBackAlley", LoadSceneMode.Single);
    }
    
    public void Back()
    {
         SceneManager.LoadScene("N_1_ActPublisherStreetNav", LoadSceneMode.Single);
    }
}
