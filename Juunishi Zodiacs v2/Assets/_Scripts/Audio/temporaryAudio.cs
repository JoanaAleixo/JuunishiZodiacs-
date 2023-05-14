using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporaryAudio : MonoBehaviour
{
    public static temporaryAudio audioTempInstance;

    private void Awake()
    {
        if (audioTempInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            audioTempInstance = this;
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
