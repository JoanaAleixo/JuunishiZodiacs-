using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessScreen : MonoBehaviour
{
    public static BrightnessScreen brightnessScreenInstance;

    private void Awake()
    {
        if (brightnessScreenInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            brightnessScreenInstance = this;
            DontDestroyOnLoad(this);
        }
    } 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
