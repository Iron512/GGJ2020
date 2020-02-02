using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Event", order = 1)]
public class Event : ScriptableObject
{
	[Tooltip("Name of the event")]
	public string name;
	
	[Multiline]
	[Tooltip("Description of the event")]
	public string description;

	public Sprite icon = null;
	[Tooltip("Duration of the event in seconds")]
	public int duration = 20;
	
	[Tooltip("Percentage debuf to population")]
	[Range(0, 1)] public float deltaPoppulation = 0;

	[Tooltip("Percentage debut to nature")]
	[Range(0, 1)] public float deltaNature = 0;
	
	[Tooltip("Percentage debuf to temperature")]
	[Range(0, 1)] public float deltaPollution= 0;
	
	[Tooltip("Percentage debuf to Water")]
	[Range(0, 1)] public float deltaWater = 0;
	
	[Tooltip("Percentage debuf to percentage")]
	[Range(0, 1)] public float deltaResources = 0;

	protected float startTime = 0; //time the event fired

	private int currentPopulationVariation = 0; //how many people have died for this event
	private int targetPopulationVariation = 0; //how many people will be dead because of this event at the end of its execution

	private int currentNatureVariation = 0; // nature score variation caused by this event up to now
	private int targetNatureVariation = 0;// nature score variation at the end of this event
	
	private int currentPollutionVariation = 0; //temperature score variation since the start of the event
	private int targetPollutionVariation = 0; //target temperature score variation at the end of the event
	
	private int currentWaterVariation = 0; //water score variation since the start of the event
	private int targetWaterVariation = 0; //target water score variation at the end of the event
	
	private int currentResourcesVariation = 0; //resources score variation since the start of the event
	private int targetResourceVariation = 0; //target resource score variation at the end of the event

	public float timeAtStart
	{
		get { return this.startTime; }
	}
	
	public void onEventStarts()
	{
		//set startTime
		this.startTime = Time.time;

		// form interactions
		GameObject form;
		form = GameObject.Find("EventForm");
		

		//set all target variations
		this.targetPopulationVariation = (int)(ResourcesManager._instance.population * (1 - this.deltaPoppulation));
		this.targetNatureVariation = (int)(ResourcesManager._instance.nature * (1 - this.deltaPoppulation));
		this.targetPollutionVariation = (int)(ResourcesManager._instance.pollution * (1 - this.deltaPoppulation));
		this.targetWaterVariation = (int)(ResourcesManager._instance.water * (1 - this.deltaPoppulation));
		this.targetResourceVariation = (int)(ResourcesManager._instance.resources * (1 - this.deltaPoppulation));
	}
	
	
	// execute the event effects lowering resources
	public void onEventExecute()
	{
		float percentage = Mathf.Min(Time.time-this.startTime/this.duration,1);

		ResourcesManager._instance.population -= this.computeResourceValue(ref this.targetPopulationVariation,
			ref this.currentPopulationVariation, percentage);

		ResourcesManager._instance.nature -= this.computeResourceValue(ref this.targetNatureVariation,
			ref this.currentNatureVariation, percentage);
		
		ResourcesManager._instance.pollution -= this.computeResourceValue(ref this.targetPollutionVariation,
			ref this.currentPollutionVariation, percentage);
		
		ResourcesManager._instance.water -= this.computeResourceValue(ref this.targetWaterVariation,
			ref this.currentWaterVariation, percentage);
		
		ResourcesManager._instance.resources -= this.computeResourceValue(ref this.targetResourceVariation,
			ref this.currentResourcesVariation, percentage);
	}

	private int computeResourceValue(ref int targetVariation, ref int currentVariation, float percentage)
	{
		int value = (int)(targetVariation*percentage- currentVariation);
		currentVariation = (int) (targetVariation * percentage);
		return value;
	}
	
	public void onEventEnd()
	{
		
	}
	
}
