using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_1_ActPublisherStreetNav : MonoBehaviour
{
   public void PublisherStreet()
    {
         SceneManager.LoadScene("D_1_Act1.3", LoadSceneMode.Single);
    }

    public void BackAlley()
    {
         SceneManager.LoadScene("N_1_ActBackAlleyNav", LoadSceneMode.Single);
    }
    public void BackAlleyFire()
    {
         SceneManager.LoadScene("D_1_Act1.5", LoadSceneMode.Single);
    }

    public void StationStreet()
    {
         SceneManager.LoadScene("N_1_ActTrainStationNav", LoadSceneMode.Single);
    }

    public void Back()
    {
         SceneManager.LoadScene("N_1_ActResidencialNav", LoadSceneMode.Single);
    }
}
