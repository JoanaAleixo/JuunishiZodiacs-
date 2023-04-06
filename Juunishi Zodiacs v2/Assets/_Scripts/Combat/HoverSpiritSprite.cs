using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSpiritSprite : MonoBehaviour
{
    public void OnHover()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UnHover()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
