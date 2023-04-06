using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_3_ActPublisherStreetNav : MonoBehaviour
{
    public void ResidentalStreet()
    {
         SceneManager.LoadScene("N_3_ActResidencialNav", LoadSceneMode.Single);
    }
}
