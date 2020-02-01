using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourcesManager : Singleton<ResourcesManager>
{
    // access to singleton:
    // ResourceManager.Instance.ResourceTestInt
    // Basic

    public float coeficient = 1f;
    public int population = 10;
    public int wellfare = 10;
    public int nature = 10;
    public float pollution = 1;
    public int water = 10;
    public int resources = 10;

    
    public float wellfareMoltiplicator = 1f;
    public float natureMoltiplicator = 1f;
    public float pollutionMoltiplicator = 1f;
    public float resourceMoltiplicator = 1f;
    
    
    
    public float getWellfareThreshold()
    {
        return (this.population / 2);
    }
    
    public float getNatureThreshold()
    {
        return (this.population / 10) * this.pollution;
    }

    public float getWaterThreshold()
    {
        return (this.population / 5 + this.getNatureThreshold() / 5) * this.pollution;
    }

    public float getPollutionThreshold()
    {
        return this.pollution;
    }

    public float getResourceThreshold()
    {
        return 0;
    }

    public float getBudget()
    {
        return this.population *this.coeficient;
    }



}