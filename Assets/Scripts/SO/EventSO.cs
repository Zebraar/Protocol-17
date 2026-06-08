using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Protocol17/Event")]
public class EventSO : ScriptableObject
{
    public string eventName;
    [TextArea]
    public string description;
    public Sprite image;
    public AudioClip sound;
    public List<EventChoice> choices;
}
