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
    public List<EventFormManager> eventFormList;

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
        foreach(var form in eventFormList)
        {
            if (form.gameObject.activeSelf == false)
            {
                // events
                //newEvent.onEventStarts();
                this.activeEvents.Add(newEvent);
                // form
                form.gameObject.SetActive(true);
                form.SetNewEvent(newEvent);
                break;
            }
        }
    }

    //Execute all the active ecents and deactivate them if they have done their time
    public void ExecuteEvents()
    {
        foreach (var ev in activeEvents)
        {
            //ev.onEventExecute();
            if (ev.timeAtStart + ev.duration >= Time.time)
            {
                foreach (var form in eventFormList)
                {
                    if (form.currentEvent == ev && form.gameObject.activeSelf == true)
                    {
                        // ev.onEventEnd();
                        activeEvents.Remove(ev);
                        form.gameObject.SetActive(false);
                    }
                }
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