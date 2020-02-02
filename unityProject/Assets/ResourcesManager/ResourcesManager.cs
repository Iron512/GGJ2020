using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : Singleton<ResourcesManager>
{

	public int population = 100;
	public TextMeshProUGUI populationText;

	public Scrollbar healthScroll;
	public Scrollbar industryScroll;
	public Scrollbar anarchyScroll;
	public Scrollbar greenScroll;
	public Scrollbar researchScroll;

	public float welfare = 50f;
	public float popularity = 50f;
	public float nature = 50f;
	public float water = 50f;
	public float production = 50f;
	public AnimationCurve gainCurve;


	private float[,] policies = {
		{0.2f,0.1f,-0.05f,-0.05f,-0.1f,0}, //salute
		{-0.1f,0.1f,-0.05f,-0.05f,0.2f,0}, //industriella
		{-0.05f,-0.05f,0.1f,0.1f,0,0}, //analchia
		{0.1f,0,0.1f,0.1f,-0.1f,0}, //green
		{-0.05f,-0.05f,-0.05f,-0.05f,-0.05f,0.5f} //ricerca
	};

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("executeTick",3,1);
    }

    void executeTick() {
    	float health = healthScroll.value;
    	float industry = industryScroll.value;
    	float anarchy = anarchyScroll.value;
    	float green = greenScroll.value;
    	float research = researchScroll.value;
        float deltaWelfare = 0;
    	float deltaPopularity = 0;
    	float deltaNature = 0;
    	float deltaWater = 0;
    	float deltaProduction = 0;

        float[] multipliers = {health, industry, anarchy, green, research};
        for (int i = 0; i < multipliers.Length; i++) {
	        deltaWelfare += policies[i, 0] * multipliers[i];
	        deltaPopularity += policies[i, 1] * multipliers[i];
	        deltaNature += policies[i, 2] * multipliers[i];
	        deltaWater += policies[i, 3] * multipliers[i];
	        deltaProduction += policies[i, 4] * multipliers[i];
        }

        welfare *= (1 + deltaWelfare);
    	popularity *= (1 + deltaPopularity);
    	nature *= (1 + deltaNature);
    	water *= (1 + deltaWater);
    	production *= (1 + deltaProduction);
        /*
        float populationFactor = (gainCurve.Evaluate((welfare-50)/50) + gainCurve.Evaluate((popularity-50)/50) + gainCurve.Evaluate((nature-50)/50) + gainCurve.Evaluate((water-50)/50) + gainCurve.Evaluate((production-50)/50));

        this.population +=(int) (this.population * Mathf.Min(populationFactor,0.05f)/4);
        populationText.text = this.population+"";
        */
        //print(population);

        // // Apply any existing even
        for (int i = 0; i < EventsManager._instance.activeEvents.Count; i++) {
            // Event event = EventsManager._instance.activeEvents[i];
            // if (event.deltaWelfare > 0.0)    welfare    = welfare    * (1 - deltaWelfare);
            // if (event.deltaPopularity > 0.0) popularity = popularity * (1 - deltaPopularity);
            // if (event.deltaNatur > 0.0)      nature     = nature     * (1 - deltaNatur);
            // if (event.deltaWater > 0.0)      water      = water      * (1 - deltaWater);
            // if (event.deltaProduction > 0.0) production = production * (1 - deltaProduction);
        }

        // draw graph
        Circle._instance.changeWelfare(welfare);
    	Circle._instance.changePopularity(popularity);
    	Circle._instance.changeNature(nature);
    	Circle._instance.changeWater(water);
    	Circle._instance.changeProduction(production);
    }



}
