using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] Vector2 initialPos;
    [SerializeField] Vector2 bottomPos;
    [SerializeField] bool goingDown;
    [SerializeField] float yeet;
    [SerializeField] float speed;

    void Start()
    {
        goingDown = true;
        initialPos = transform.position;
        bottomPos = transform.position - new Vector3(0, -1, 0);
    }

    
    void Update()
    {
        yeet += Time.deltaTime / speed;
        if (goingDown)
        {
            transform.position = Vector2.Lerp(initialPos, bottomPos, yeet);
        }
        else
        {
            transform.position = Vector2.Lerp(bottomPos, initialPos, yeet);
        }

        if(yeet >= 1)
        {
            ChangeGoingDown();
        }
    }

    private void ChangeGoingDown()
    {
        yeet = 0;
        goingDown = !goingDown;
    }
}
