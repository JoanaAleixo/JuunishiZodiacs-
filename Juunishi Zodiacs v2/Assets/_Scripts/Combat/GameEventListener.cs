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
