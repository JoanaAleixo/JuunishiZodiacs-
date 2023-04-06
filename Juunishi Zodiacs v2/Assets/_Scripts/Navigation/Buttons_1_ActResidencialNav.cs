using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_1_ActResidencialNav : MonoBehaviour
{
    public void FushimiHouse()
    {
         SceneManager.LoadScene("Nav_1_ActResidencial.p2", LoadSceneMode.Single);
    }
    public void AkiraHouse()
    {
         SceneManager.LoadScene("Nav_1_ActResidencial.p1", LoadSceneMode.Single);
    }
    public void PublisherStreet()
    {
         SceneManager.LoadScene("N_1_ActPublisherStreetNav", LoadSceneMode.Single);
    }
}
