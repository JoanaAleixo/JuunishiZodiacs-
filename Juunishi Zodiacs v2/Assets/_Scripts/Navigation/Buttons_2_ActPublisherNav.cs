using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons_2_ActPublisherNav : MonoBehaviour
{
    public void Elevator()
    {
         SceneManager.LoadScene("D_1_Act2.3", LoadSceneMode.Single);
    }
}
