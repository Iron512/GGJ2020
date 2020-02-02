using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Policies", menuName = "Policies", order = 1)]
public class Policy : ScriptableObject
{
	[Tooltip("Name of the policy")]
	public string name;

	[Multiline]
	[Tooltip("Description of the policy")]
	public string description;

	[Tooltip("Buffering before policy change in seconds")]
	public int buffering = 20;

	[Tooltip("Percentage buff or debuff to population")]
	[Range(-1, 1)] public float deltaPopulation = 0.0f;

	[Tooltip("Percentage buff or debuff to nature")]
	[Range(-1, 1)] public float deltaNature = 0.0f;

	[Tooltip("Percentage buff or debuff to temperature")]
	[Range(-1, 1)] public float deltaTemperature = 0.0f;

	[Tooltip("Percentage buff or debuff to Water")]
	[Range(-1, 1)] public float deltaWater = 0.0f;

	[Tooltip("Percentage buff or debuff to natural resources")]
	[Range(-1, 1)] public float deltaResources = 0.0f;

	
	
	public void onPolicyStart()
	{
		// apply policy modificators
		/*ResourcesManager._instance.wellfareMoltiplicator += deltaPopulation;
		ResourcesManager._instance.natureMoltiplicator += deltaNature;
		ResourcesManager._instance.pollutionMoltiplicator += deltaTemperature;
		ResourcesManager._instance.waterMoltiplicator += deltaWater;
		ResourcesManager._instance.resourceMoltiplicator+= deltaWater;*/
	}
	public void onPolicyEnd()
	{
		// remove policy modificators
		/*ResourcesManager._instance.wellfareMoltiplicator -= deltaPopulation;
		ResourcesManager._instance.natureMoltiplicator -= deltaNature;
		ResourcesManager._instance.pollutionMoltiplicator -= deltaTemperature;
		ResourcesManager._instance.waterMoltiplicator -= deltaWater;
		ResourcesManager._instance.resourceMoltiplicator -= deltaWater;*/
	}
	
	
}