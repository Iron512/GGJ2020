using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    private static EventsManager _instance;
    public float tick = 1; //time interval in seconds between the update 
    public float secondsToEventStart = 5; // time interval in seconds between each event
    List<Event> activeEvents = new List<Event>(); //list containing all the events currently in execution
    public List<Event> availableEvents;

    public static EventsManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public void Start()
    {
        print("start eventmanager");
        InvokeRepeating("ExecuteEvents", 1, tick);
        InvokeRepeating("FireRandomEvent", 1, secondsToEventStart);
    }

    // starts a random event
    public void FireRandomEvent()
    {
        Event eventToStart = GetEvent();
        FireEvent(eventToStart);
    }
    //add a new event to the event that are executing
    public void FireEvent(Event newEvent)
    {
        newEvent.onEventStarts();
        this.activeEvents.Add(newEvent);
    }

    //Execute all the active ecents and deactivate them if they have done their time
    public void ExecuteEvents()
    {
        foreach (var ev in activeEvents)
        {
            ev.onEventExecute();
            if (ev.timeAtStart + ev.duration >= Time.time)
            {
                ev.onEventEnd();
                activeEvents.Remove(ev);
            }
        }
    }
    public Event GetEvent()
    {
        // private Utility utility = new Utility;
        var random = new System.Random();
        int randomIndex = random.Next(availableEvents.Count);
        return availableEvents[randomIndex];
    }
}