using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] Vector2 initialPos;
    [SerializeField] float verticalSpeed;
    [SerializeField] float amplitude;
    [SerializeField] float pushDown;
    [SerializeField] float displace;

    void Start()
    {
        initialPos = transform.position;
    }

    
    void FixedUpdate()
    {
        initialPos.y = (Mathf.Sin((Time.realtimeSinceStartup + displace) * verticalSpeed) * amplitude) - pushDown;
        transform.position = initialPos;
    }

}
