using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons_3_ActRoofTopNav : MonoBehaviour
{
     public void GoingDown()
    {
         SceneManager.LoadScene("N_3_ActPublisherNav", LoadSceneMode.Single);
    }
}
