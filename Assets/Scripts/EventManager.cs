using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    PlayerTurnChanged,
    DiceRolled,
    PlayerMoved,
    PlayerBumped,
    MouseClick,
    GameStarted,
    GameEnded
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    
    private Dictionary<EventType, System.Action<EventType>> eventListeners = new();
    void Awake()
    {
        Instance = this;
    }
    
    public void AddEvent(EventType eventType)
    {
        eventListeners.Add(eventType, null);
    }

    public void FireEvent(EventType eventType)
    {
        if (!eventListeners.ContainsKey(eventType))
        {
            AddEvent(eventType);
        }

        if (eventListeners[eventType] != null)
        {
            eventListeners[eventType](eventType);
        }
    }
    
    public void SubscribeToEvent(EventType eventType, System.Action<EventType> listener)
    {
        if (eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] += listener;
        }
        else
        {
            eventListeners[eventType] = listener;
        }
    }
}
