using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip diceClip;
    [SerializeField] private AudioClip moveClip;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioSource music;

    private Dictionary<EventType, AudioClip> _clips;
    void Start()
    {
        Instance = this;
        
        _clips = new Dictionary<EventType, AudioClip>()
        {
            { EventType.DiceRolled, diceClip },
            { EventType.PlayerMoved, moveClip },
            { EventType.MouseClick, clickClip },
            { EventType.PlayerBumped, hitClip },
            
        };
        
        foreach (var eventType in _clips.Keys)
        {
            EventManager.Instance.SubscribeToEvent(eventType, PlaySoundForEvent);
        }
        music.Play();
    }

    private void PlaySoundForEvent(EventType eventType)
    {
        source.clip = _clips[eventType];
        source.Play();
    }
}
