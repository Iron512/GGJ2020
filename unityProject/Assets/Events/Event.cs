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

	[Tooltip("Duration of the event in seconds")]
	public int duration = 20;
	
	[Tooltip("Percentage debuf to population")]
	[Range(0, 1)] public float deltaPoppulation = 0;

	[Tooltip("Percentage debut to nature")]
	[Range(0, 1)] public float deltaNature = 0;
	
	[Tooltip("Percentage debuf to temperature")]
	[Range(0, 1)] public float deltaTemperature= 0;
	
	[Tooltip("Percentage debuf to Water")]
	[Range(0, 1)] public float deltaWater = 0;
	
	[Tooltip("Percentage debuf to percentage")]
	[Range(0, 1)] public float deltaResources = 0;

	public void onEventStart()
	{
		
	}
	
	public void onEventEnd()
	{
		
	}
}
