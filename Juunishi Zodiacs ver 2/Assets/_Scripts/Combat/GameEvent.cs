using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        Debug.Log(listeners.Count);
        for(int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
            Debug.Log("Raised1");
        }
    }
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
        Debug.Log("Register Added");
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
