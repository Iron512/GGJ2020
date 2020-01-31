using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourcesManager : Singleton<ResourcesManager>
{
    // access to singleton:
    // ResourceManager.Instance.ResourceTestInt
    // Basic
    public int population = 10;
    public int nature = 10;
    public int globalTemperature = 10;
    public int water = 10;
    public int naturalResources = 10;

    // Politics
    // TODO: classe che permetta la selezione di un solo ambito

}