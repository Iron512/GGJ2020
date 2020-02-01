using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes2D;

public class Circle : MonoBehaviour {
    // Start is called before the first frame update
 	public float radius = 5f;
 	public float thickness = 0.2f;
 	public float maxValue = 100;

 	private int vertex = 3;
	private GameObject[] objects;
	private Shapes2D.Shape[] lines;
	private Shapes2D.Shape[] donuts;
 	private int[] position;
 	private Shapes2D.Shape curve;
 	private Shapes2D.Shape curve2;

    private void Start() {
    	objects = new GameObject[11];
    	lines = new Shapes2D.Shape[5];
    	donuts = new Shapes2D.Shape[5];
    	position = new int[5];

    	for (int i = 0; i < 11; i++) {
    		objects[i] = gameObject.transform.GetChild(i).gameObject;
    	}
		
    	for (int i = 0; i < 5; i++) {
    		position[i] = 100;

    		lines[i] = objects[i].GetComponent<Shapes2D.Shape>();
    		lines[i].enabled = false;
			lines[i].settings.fillColor = Color.red;
  			lines[i].transform.position = new Vector3(0,0,-1);
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
  		drawDonuts();
  		drawCurve();
    }

    private void drawStar() {
    	int step = 360/vertex;

    	for (int i=0; i<vertex; i++) {
    		int deg = step*i;

    		lines[i].enabled = true;
    		lines[i].transform.position = new Vector3(0,0,-1) + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * deg)*radius/2,Mathf.Cos(Mathf.Deg2Rad * deg)*radius/2,0);
    		lines[i].transform.rotation = Quaternion.Euler(0,0,deg);
    	}
    }

    private void drawDonuts() {
   		int step = 360/vertex;

    	for (int i=0; i<vertex; i++) {
    		int deg = step*i;

    		donuts[i].enabled = true;
    		donuts[i].transform.position = new Vector3(0,0,-1) +  new Vector3(-Mathf.Sin(Mathf.Deg2Rad * deg)*radius/maxValue*position[i],Mathf.Cos(Mathf.Deg2Rad * deg)*radius/maxValue*position[i],0);
    	}

    }

    private void drawCurve() {
		Vector2[] points = new Vector2[vertex];
		PathSegment[] points2 = new PathSegment[vertex];
    	curve.enabled = true;
    	curve2.enabled = true;

    	for (int i = 0; i < vertex; i++) {
			points[i] = new Vector2(donuts[i].transform.position.x/radius/2,donuts[i].transform.position.y/radius/2);
    		points2[i] = new PathSegment( 
    			new Vector2(donuts[i].transform.position.x/radius/2+0.1f,
    						donuts[i].transform.position.y/radius/2-0.05f), 
    			new Vector2(donuts[i].transform.position.x/radius/2,
    						donuts[i].transform.position.y/radius/2+0.05f), 
    			new Vector2(donuts[i].transform.position.x/radius/2-0.1f,
    				        donuts[i].transform.position.y/radius/2-0.05f));
    	}

    	curve.settings.polyVertices = points;
    	curve2.settings.pathSegments = points2;
    }

    public void Update() {
    	bool changes = false;

    	//FIRST
		if (Input.GetKey(KeyCode.Q) && position[0] < 100) {
			position[0]++;
			changes = true;
        }
		if (Input.GetKey(KeyCode.A) && position[0] > 0) {
			position[0]--;
			changes = true;
        }

		//SECOND
		if (Input.GetKey(KeyCode.W) && position[1] < 100) {
			position[1]++;
			changes = true;
        }
		if (Input.GetKey(KeyCode.S) && position[1] > 0) {
			position[1]--;
			changes = true;
        }
    	//THIRD
		if (Input.GetKey(KeyCode.E) && position[2] < 100) {
			position[2]++;
			changes = true;
        }
		if (Input.GetKey(KeyCode.D) && position[2] > 0) {
			position[2]--;
			changes = true;
        }

		//FOURTH
		if (Input.GetKey(KeyCode.R) && position[3] < 100) {
			position[3]++;
			changes = true;
        }
		if (Input.GetKey(KeyCode.F) && position[3] > 0) {
			position[3]--;
			changes = true;
        }

		//FIFTH
		if (Input.GetKey(KeyCode.T) && position[4] < 100) {
			position[4]++;
			changes = true;
        }
		if (Input.GetKey(KeyCode.G) && position[4] > 0) {
			position[4]--;
			changes = true;
        }


		if (Input.GetKeyDown(KeyCode.Space) && vertex < 5) {
			vertex++;
			changes = true;
        }

        if (changes) {
	  		drawStar();
	  		drawDonuts();
	  		drawCurve();
        }
    }
}
