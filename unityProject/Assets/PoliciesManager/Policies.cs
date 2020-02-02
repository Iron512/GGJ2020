using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Policies", menuName = "Policies", order = 1)]
public class Policies : ScriptableObject
{
	
	[Tooltip("Name of the policy")]
	public string name;

	[Multiline]
	[Tooltip("Description of the policy")]
	public string description;

	[Tooltip("Buffering before policy change in seconds")]
	public int buffering = 20;

	[Tooltip("Percentage buff or debuff to population")]
	[Range(-1, 1)] public float deltaPoppulation = 0;

	[Tooltip("Percentage buff or debuff to nature")]
	[Range(-1, 1)] public float deltaNature = 0;

	[Tooltip("Percentage buff or debuff to temperature")]
	[Range(-1, 1)] public float deltaTemperature = 0;

	[Tooltip("Percentage buff or debuff to Water")]
	[Range(-1, 1)] public float deltaWater = 0;

	[Tooltip("Percentage buff or debuff to natural resources")]
	[Range(-1, 1)] public float deltaResources = 0;

	public void onEventStarts()
	{
		//TODO: change manteinance costs according to deltas
	}
}