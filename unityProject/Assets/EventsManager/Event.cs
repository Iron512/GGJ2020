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
	[Range(0, 1)] public float deltaWelfare = 0;

	[Tooltip("Percentage debut to popularity")]
	[Range(0, 1)] public float deltaPopularity = 0;
	
	[Tooltip("Percentage debuf to nature")]
	[Range(0, 1)] public float deltaNature= 0;
	
	[Tooltip("Percentage debuf to Water")]
	[Range(0, 1)] public float deltaWater = 0;
	
	[Tooltip("Percentage debuf to production")]
	[Range(0, 1)] public float deltaProduction = 0;

	protected float startTime = 0; //time the event fired

	private int currentWelfareVariation = 0; //how many people have died for this event
	private int targetWelfareVariation = 0; //how many people will be dead because of this event at the end of its execution
	
	private int currentPopularityVariation = 0; //temperature score variation since the start of the event
	private int targetPopularityVariation = 0; //target temperature score variation at the end of the event

	private int currentNatureVariation = 0; // nature score variation caused by this event up to now
	private int targetNatureVariation = 0;// nature score variation at the end of this event
	
	private int currentWaterVariation = 0; //water score variation since the start of the event
	private int targetWaterVariation = 0; //target water score variation at the end of the event
	
	private int currentProductionVariation = 0; //resources score variation since the start of the event
	private int targetProducitonVariation = 0; //target resource score variation at the end of the event

	public float timeAtStart
	{
		get { return this.startTime; }
	}
	
	public void onEventStarts()
	{
		//set startTime
		this.startTime = Time.time;

		/*
		//set all target variations
		this.targetWelfareVariation = (int)(ResourcesManager._instance.welfare * (1 - this.deltaWelfare));
		this.targetPopularityVariation = (int)(ResourcesManager._instance.popularity * (1 - this.deltaPopularity));
		this.targetNatureVariation = (int)(ResourcesManager._instance.nature * (1 - this.deltaNature));
		this.targetWaterVariation = (int)(ResourcesManager._instance.water * (1 - this.deltaWater));
		this.deltaProduction = (int)(ResourcesManager._instance.production * (1 - this.deltaProduction));
		*/
	}
	
	
	// execute the event effects lowering resources
	public void onEventExecute()
	{
		/*
		float percentage = Mathf.Min(Time.time-this.startTime/this.duration,1);

		ResourcesManager._instance.welfare -= this.computeResourceValue(ref this.targetWelfareVariation,
			ref this.currentWelfareVariation, percentage);

		ResourcesManager._instance.popularity -= this.computeResourceValue(ref this.targetPopularityVariation,
			ref this.currentPopularityVariation, percentage);

		ResourcesManager._instance.nature -= this.computeResourceValue(ref this.targetNatureVariation,
			ref this.currentNatureVariation, percentage);
		
		ResourcesManager._instance.water -= this.computeResourceValue(ref this.targetWaterVariation,
			ref this.currentWaterVariation, percentage);
		
		ResourcesManager._instance.production -= this.computeResourceValue(ref this.targetProducitonVariation,
			ref this.currentProductionVariation, percentage);
			*/
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
