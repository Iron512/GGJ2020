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

    // Extra expenses
    public int research = 10;
    public int instruction = 10;

    // Techs
    // Population
    public bool vaccine = false;
    public bool stimolant = false;
    // Nature
    public bool permacolture = false;
    public bool GMO = false;
    public bool hydroponic = false;
    // Global Temperature
    public bool CO2Capture = false;
    public bool industrialCatalyst = false;
    public bool populaitonHabits = false;
    // Water
    public bool depuration = false;
    public bool smartTreatment = false;
    public bool ferragni = false;
    // Natural Resources
    public bool renewableEnergies = false;
    public bool newDeposits = false;
    public bool reuseAncientRubbishDump = false;    
}