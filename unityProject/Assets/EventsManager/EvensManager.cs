using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private static EventManager _instance;
    public float tick = 1; //time interval in seconds between the update 
    List<Event> activeEvents = new List<Event>(); //list containing all the events currently in execution

    public static EventManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    
    public void Start()
    {
        InvokeRepeating("ExecuteEvents", 1, 1);
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
    
    
}