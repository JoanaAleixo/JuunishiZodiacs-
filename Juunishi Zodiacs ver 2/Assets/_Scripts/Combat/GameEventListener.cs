using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent curEvent;
    [SerializeField] UnityEvent response;

    private void OnEnable()
    {
        curEvent.RegisterListener(this);
        Debug.Log("Registered");
    }

    private void OnDisable()
    {
        curEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();  
    }
}
