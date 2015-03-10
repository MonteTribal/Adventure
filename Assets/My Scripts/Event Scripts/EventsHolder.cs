using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventSpace;

public class EventsHolder : MonoBehaviour
{
    private List<GameEvent> events = new List<GameEvent>();

    void Start()
    {
        loadDefaults();
    }

    public List<GameEvent> getAllEvents()
    {
        return events;
    }
    
    public GameEvent getEvent(int id)
    {
        try
        {
            return events[id];
        }
        catch(System.Exception e)
        {
            print("Is there an event numbered: " + id.ToString() + "?");
            return new GameEvent();
        }
    }
    
    public GameEvent getEvent(string name)
    {
        foreach (GameEvent evt in events)
        {
            if(evt.getName() == name)
            {
                return evt;
            }
        }
        
        return new GameEvent("NULL", "NULL");
    }
    
    private bool checkForDuplicate(GameEvent ev)
    {
        if (events.Contains(ev))
        {
            return true;
        }
        
        foreach (GameEvent evt in events)
        {
            if(evt.getName() == ev.getName())
            {
                return true;
            }
        }
        
        return false;
    }
    
    
    public void addEvent(GameEvent newEvent)
    {
        if(checkForDuplicate(newEvent))
        {
            Debug.LogWarning("Cannot have duplicate Event: " + newEvent.getName());                   
            return;
        }
        
        newEvent.setId(events.Count);
        events.Add(newEvent);
    }

    public void clearEvents()
    {
        events.Clear();
    }

    private void loadDefaults()
    {
        EventSpace.EventLoader a = new EventLoader();
        a.loadEventsArea0();
    }
}