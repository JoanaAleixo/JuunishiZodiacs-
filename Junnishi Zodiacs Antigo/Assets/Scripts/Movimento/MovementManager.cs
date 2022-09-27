using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] GameObject cameraMover;

    [SerializeField] Transform posi�aoCamera;
    [SerializeField] GameObject[] cenarios;

    public GameObject CameraMover { get => cameraMover; set => cameraMover = value; }
    public Transform Posi�aoCamera { get => posi�aoCamera; set => posi�aoCamera = value; }

    public void BotaoMovimento(int cenario)
    {
        Posi�aoCamera = cenarios[cenario].transform;
        CameraMover.transform.position = new Vector3 (Posi�aoCamera.transform.position.x, Posi�aoCamera.transform.position.y, -10) ;
    }
}
