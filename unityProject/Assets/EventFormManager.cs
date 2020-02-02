using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventFormManager : MonoBehaviour
{
    public Text descriptionLabel;
    public Text nameLabel;
    public Image icon;
    public Event currentEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetNewEvent(Event newEvent)
    {
        this.descriptionLabel.text = newEvent.description;
        this.nameLabel.text = newEvent.name;
        this.icon.sprite = newEvent.icon;
        this.currentEvent = newEvent;
    }
}
