using System;
using System.Collections.Generic;

[Serializable]
public class EventChoice
{
    public CardSO card;
    public EventSO nextEvent;
    public List<FlagSO> flagsToSet;
    public List<FlagSO> requiredFlags;
}
