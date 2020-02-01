using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourcesManager : Singleton<ResourcesManager> {

    private int welfare = 50;
    private int nature = 50;
    private int water = 50;
    private int production = 50;
    private int popularity = 50;

    private int[,] policies = new int {
        {20;10;-5;-5;-10},
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,0,0,0},
        {-10;-10;-10;-10;-10}
    };

    public void Start() {
        InvokeRepeating("executeTick",2,1);
    }

    public void executeTick() {

    }
}