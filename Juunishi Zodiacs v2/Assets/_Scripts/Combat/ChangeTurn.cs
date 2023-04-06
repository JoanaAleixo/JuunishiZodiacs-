using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTurn : MonoBehaviour
{
    [SerializeField] bool isPlayerTurn;
    [SerializeField] bool doAnimation;
    [SerializeField] TextMeshPro text;
    int i = 0;

    void Start()
    {
        isPlayerTurn = true;
    }

    public void ChangeTurnPrompt()
    {
        doAnimation = true;
        i = 0;
        if (isPlayerTurn)
        {
            text.text = "Enemy Turn";
        }
        else
        {
            text.text = "Player Turn";
        }
    }

    private void Update()
    {
        if (doAnimation)
        {
            if (isPlayerTurn)
            {
                if (i == 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1, 0), Time.deltaTime*20);
                    if (transform.position.x >= -1)
                    {
                        i = 1;
                    }
                }
                if (i == 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(1, 0), Time.deltaTime);
                    if (transform.position.x >= 1)
                    {
                        i = 2;
                    }
                }
                if (i == 2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(20, 0), Time.deltaTime*20);
                    if (transform.position.x >= 20)
                    {
                        doAnimation = false;
                        isPlayerTurn = false;
                    }
                }
            }
            else
            {
                if (i == 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(1, 0), Time.deltaTime*20);
                    if (transform.position.x <= 1)
                    {
                        i = 1;
                    }
                }
                if (i == 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1, 0), Time.deltaTime);
                    if (transform.position.x <= -1)
                    {
                        i = 2;
                    }
                }
                if (i == 2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(-20, 0), Time.deltaTime*20);
                    if (transform.position.x <= -20)
                    {
                        doAnimation = false;
                        isPlayerTurn = true;
                    }
                }
            }
        } 
        
    }
}
