using System.Collections.Generic;
using UnityEngine;
using Shapes2D;

public class Circle : Singleton<Circle> {
    // Start is called before the first frame update
 	public float radius = 4f;
 	public float thickness = 0.2f;
 	public float maxValue = 100;

	Vector3 startPosition = new Vector3(0f,0f,-1f);

 	private int vertex = 5;
	private static GameObject[] objects;
	private static Shapes2D.Shape[] lines;
	private static Shapes2D.Shape[] donuts;
 	public static float[] positions;
 	private Shapes2D.Shape curve;
 	private Shapes2D.Shape curve2;

    private void Start()
    {
	    startPosition = this.transform.position;
        objects = new GameObject[11];
        lines = new Shapes2D.Shape[5];
        donuts = new Shapes2D.Shape[5];
        positions = new float[5];

    	for (int i = 0; i < 11; i++) {
    		objects[i] = gameObject.transform.GetChild(i).gameObject;
    	}
		
    	for (int i = 0; i < 5; i++) {
    		positions[i] = 50f;

    		lines[i] = objects[i].GetComponent<Shapes2D.Shape>();
    		lines[i].enabled = false;
			lines[i].settings.fillColor = Color.gray;
  			lines[i].transform.position = startPosition;
  			lines[i].transform.localScale = new Vector3(thickness,radius,0);

  			donuts[i] = objects[i+5].GetComponent<Shapes2D.Shape>();
    		donuts[i].enabled = false;
  			donuts[i].settings.fillColor = Color.blue;
  		}

  		curve = (gameObject.transform.GetChild(10).gameObject).GetComponent<Shapes2D.Shape>();
  		
  		curve.enabled = false;

  		curve2 = (gameObject.transform.GetChild(11).gameObject).GetComponent<Shapes2D.Shape>();
  		curve2.enabled = false;

  		drawStar();
  		//drawDonuts();
  		drawCurve();
    }

    private void drawStar() {
    	int step = 360/vertex;

    	for (int i=0; i<vertex; i++) {
    		int deg = step*i;

    		lines[i].enabled = true;
    		lines[i].transform.position = startPosition + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * deg)*radius/2,Mathf.Cos(Mathf.Deg2Rad * deg)*radius/2,0);
    		lines[i].transform.rotation = Quaternion.Euler(0,0,deg);
    	}
    }

    private void drawDonuts() {
   		int step = 360/vertex;

    	for (int i=0; i<vertex; i++) {
    		int deg = step*i;
    		donuts[i].enabled = false;
    		donuts[i].transform.position = startPosition + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * deg)*radius/maxValue*positions[i],Mathf.Cos(Mathf.Deg2Rad * deg)*radius/maxValue*positions[i],0);
    	}

    }

    private void drawCurve() {
		Vector2[] points = new Vector2[vertex];
		PathSegment[] points2 = new PathSegment[vertex];
    	curve.enabled = true;
    	//curve2.enabled = true;

    	for (int i = 0; i < vertex; i++) {
			points[i] = new Vector2((donuts[i].transform.position.x-startPosition.x)/radius/2,(donuts[i].transform.position.y-startPosition.y)/radius/2);
    	}

    	curve.settings.polyVertices = points;
    }

    public void changeWelfare(float n) {
        positions[0] = n;

       if (positions[0] > 100)
            positions[0] = 100;
    
        if (positions[0] < 0)
            positions[0] = 0;
    }

    public void changePopularity(float n) {
        positions[1] = n;
        if (positions[1] > 100)
            positions[1] = 100;
    
        if (positions[1] < 0)
            positions[1] = 0;
    }

    public void changeNature(float n) {
        positions[2] = n;
        if (positions[2] > 100)
            positions[2] = 100;
    
        if (positions[2] < 0)
            positions[2] = 0;
    }

    public void changeWater(float n) {
        positions[3] = n;
        if (positions[3] > 100)
            positions[3] = 100;
    
        if (positions[3] < 0)
            positions[3] = 0;
    }

    public void changeProduction(float n) {
        positions[4] = n;
        if (positions[4] > 100)
            positions[4] = 100;
    
        if (positions[4] < 0)
            positions[4] = 0;
    }

    public void Update() {
        drawDonuts();
        drawCurve();
    }
}
