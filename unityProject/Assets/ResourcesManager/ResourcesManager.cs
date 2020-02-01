using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourcesManager : Singleton<ResourcesManager>
{
    // access to singleton:
    // ResourceManager.Instance.ResourceTestInt
    // Basic
    public int population = 10;
    public int wellfare = 10;
    public int nature = 10;
    public float pollution = 1;
    public int water = 10;
    public int resources = 10;
    
    public float getPopulationThreshold()
    {
        return this.population / 2;
    }

    public float getNatureThreshold()
    {
        return (this.population / 10 * this.pollution) * this.getPollution();
    }

    public float getWaterThreshold()
    {
        return (this.population / 5 + this.getNatureThreshold() / 5) * this.getPollution();
    }

    public float getPollutionThreshold()
    {
        return this.pollution;
    }

    public float getResourceThreshold()
    {
        return 0;
    }
}