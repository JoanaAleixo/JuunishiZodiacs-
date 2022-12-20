using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] GameObject cameraMover;

    [SerializeField] Transform posiçaoCamera;
    [SerializeField] GameObject[] cenarios;

    public GameObject CameraMover { get => cameraMover; set => cameraMover = value; }
    public Transform PosiçaoCamera { get => posiçaoCamera; set => posiçaoCamera = value; }

    public void BotaoMovimento(int cenario)
    {
        PosiçaoCamera = cenarios[cenario].transform;
        CameraMover.transform.position = new Vector3 (PosiçaoCamera.transform.position.x, PosiçaoCamera.transform.position.y, -10) ;
    }
}
