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

	public int duration = 20;
	[Range(0, 1)] public float deltaPoppulation;
	[Range(0, 1)] public float deltaIndustry;
	[Range(0, 1)] public float deltaClimate;
}
