using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourcesManager : Singleton<ResourcesManager>
{
    // access to singleton:
    // ResourceManager.Instance.ResourceTestInt
    // Basic

    //public float coeficient = 1f;

    public int population = 1000; // population count, impacts directly wellfare, natrue, water, resources
    
    //players parameters
    [Range(0, 1)] public float wellfare = .25f; // impacts one the population growth
    [Range(0, 1)] public float nature = .25f; // lower the pollution
    [Range(0, 1)] public float water = .25f; // impacts on the population and the nature
    [Range(0, 1)] public float resources = .25f; // impacts on the population
    //[Range(0, 1)] public float education = .2f; // impacts on the thresholds of nature water and resources
    [Range(0, 1)] public float reasearch = .2f; // ammount of the economy dedicated to the research, allows to unlock new technologies


    [Tooltip("How important is the walfare for the population growth?")]
    [Range(0, 1)] public float wellfareImportance = .4f;
    [Tooltip("How important is the nature for the population growth?")]
    [Range(0, 1)] public float natureImportance = .2f;
    [Tooltip("How important is the water for the population growth?")]
    [Range(0, 1)] public float waterImportance = .2f;
    [Tooltip("How important is the resource for the population growth?")]
    [Range(0, 1)] public float resourcesImportance = .2f;
    
    //pollution level
    [Range(0, 1)]
    public float pollution = 0f; //hit negatively nature, water, resources, wellfare 
    
    public AnimationCurve resourceImpact;

    public AnimationCurve pollutionImpact;

    public AnimationCurve natureBenefit;
    // Moltiplicators
    public float wellfareMoltiplicator = 1f;
    public float natureMoltiplicator = 1f;
    public float pollutionMoltiplicator = 1f;
    public float waterMoltiplicator = 1f;
    public float resourceMoltiplicator = 1f;

    public void Start()
    {
        InvokeRepeating("executeTick",2,3);
    }

    public void executeTick()
    {
        //update the population 
        //this.population = (int)(this.population + (this.wellfare - this.getWellfareThreshold()*this.population/10));
        float pollutionFactor = pollutionImpact.Evaluate(pollution);
        
        float wellfareFactor =  wellfareImportance*applyPollution(resourceImpact.Evaluate(this.wellfare / this.idealWellfate),pollutionFactor);
        float waterFactor =  waterImportance*applyPollution(resourceImpact.Evaluate(this.water / this.idealWater),pollutionFactor);
        float resourceFactor =  resourcesImportance*applyPollution(resourceImpact.Evaluate(this.resources / this.idealResources),pollutionFactor);
        
        float populationFactor =wellfareFactor + waterFactor + resourceFactor;
        
        float natureFactor = natureImportance*resourceImpact.Evaluate(this.nature / this.idealNature);

        if (this.pollution > 0)
        {
            this.pollution -= Mathf.Max(0.01f, this.natureBenefit.Evaluate(this.nature)*this.nature);
            if (this.pollution < 0)
            {
                this.pollution = 0;
            }
        }
        
        this.population = (int)(this.population + this.population / 30 * populationFactor);
        string s = this.population + "," + populationFactor + "," + wellfareFactor + "," + waterFactor + "," +
                   natureFactor + "," + resourceFactor;
        
            }

    private float applyPollution(float factor,float pollution)
    {
        if (factor < 0)
        {
            return factor * (pollution + 1);
        }
        else
        {
            return factor * (1-pollution);
        }
    }
    public float idealNature
    {
        get { return (0.25f); }
    }

    public float idealWellfate
    {
        get{ return  (0.25f); }
    }
    
    public float idealPollution
    {
        get{return 0; }
    }

    public float idealWater
    {
        get { return (.25f); }
    }

    public float idealResources
    {
        get { return (.25f); }
    }
}