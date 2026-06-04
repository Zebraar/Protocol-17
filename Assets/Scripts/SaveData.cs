using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int energy;
    public List<string> flags = new();
    public EventSO currentEvent;
}