using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    Vector3 finalPos;
    float timer;
    float change;
    void Start()
    {
        finalPos = transform.position + new Vector3(0, 0.5f, 0);
        change = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, finalPos, Time.deltaTime/6);
        if (timer >= 2)
        {
            change -= Time.deltaTime;
            gameObject.GetComponent<TextMeshPro>().color = new Color(GetComponent<TextMeshPro>().color.r, GetComponent<TextMeshPro>().color.g, GetComponent<TextMeshPro>().color.b, change);
        }

        if(transform.position == finalPos)
        {
            Destroy(gameObject);
        }
    }
}
