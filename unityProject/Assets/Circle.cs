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
    private Bezier bezier;
 	private int[] position;
 	private Shapes2D.Shape curve;
 	private Shapes2D.Shape curve2;

    private void Start() {
    	objects = new GameObject[13];
    	lines = new Shapes2D.Shape[5];
    	donuts = new Shapes2D.Shape[5];
    	position = new int[5];

    	for (int i = 0; i < objects.Length; i++) {
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

        bezier = objects[12].GetComponent<Bezier>();

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

    private float Norm(float[] v) {
        float result = 0;

        for (int j = 0; j < v.Length; j++) {
            result += Mathf.Pow(v[j], 2);
        }

        return Mathf.Sqrt(result);
    }

    private float Arg(float[] v) {
        return Mathf.Atan2(v[1], v[0]);
    }

    private float Arg(float[,] v, int index) {
        return Mathf.Atan2(v[index, 1], v[index, 0]);
    }

    private static float[] VelocityParameters(float theta, float phi) {
        // From "Smooth, easy to compute interpolating splines" by John D. Hobby
        // <http://www.springerlink.com/content/p4w1k8w738035w80/>

        float[] RhoSigma;
        RhoSigma = new float[2];

        float a = 1.597f;
        float b = 0.07f;
        float c = 0.37f;
        float st = Mathf.Sin(theta);
        float ct = Mathf.Cos(theta);
        float sp = Mathf.Sin(phi);
        float cp = Mathf.Cos(phi);
        float alpha = a*(st-b*sp)*(sp-b*st)*(ct-cp);
        float rho   = (2+alpha)/(1+(1-c)*ct+c*cp);
        float sigma = (2-alpha)/(1+(1-c)*cp+c*ct);

        RhoSigma[0] = rho;
        RhoSigma[1] = sigma;

        return RhoSigma;
    }

    private float[,] ComputeBezierPassingThrough(Vector2[] points) {
        // float[] x_coords = {1, 0, 0, 1, 1};
        // float[] y_coords = {0, -1, 1, 1, 0};
        //Console.WriteLine(x_coords[1]);

        // // Make sure the arrays have the same length
        // Debug.Assert(vertex + 1 == y_coords.Length);

        // // Make sure the last point is equal to the first one
        // Debug.Assert(x_coords[0] == x_coords[vertex + 1-1]);
        // Debug.Assert(y_coords[0] == y_coords[vertex + 1-1]);

        // Declare 2D arrays
        float[,] curve_directions = new float[vertex + 1,3];
        float[,] curve_points = new float[vertex + 1,3];
        var input_tension = new float[vertex + 1];
        var output_tension = new float[vertex + 1];

        // Populate arrays
        for (int i = 0; i < vertex; i++) {
            input_tension[i] = 1;
            output_tension[i] = 1;
            for (int j = 0; j < 3; j++) {
                curve_directions[i, j] = float.NaN;
            }
            curve_points[i, 0] = points[i].x;
            curve_points[i, 1] = points[i].y;
            curve_points[i, 2] = 0;
        }

        // Copy last vertex from first vertex
        curve_points[vertex, 0] = curve_points[0, 0];
        curve_points[vertex, 1] = curve_points[0, 1];
        curve_points[vertex, 2] = curve_points[0, 2];

        // Fixup vectors iff necessary
        float[] temp = new float[vertex + 1];
        for (int j = 0; j < 3; j++) {
            temp[j] = curve_points[1, j] - curve_points[vertex + 1 - 2, j];
        }

        float temp_norm = Norm(temp);

        for (int j = 0; j < 3; j++) {
            curve_directions[0,                   j] = temp[j] / temp_norm;
            curve_directions[vertex + 1 - 1, j] = temp[j] / temp_norm;
            //Console.WriteLine(curve_directions[0,j]);
        }

        for (int i = 1; i < vertex + 1 - 1; i++) {
            for (int j = 0; j < 3; j++) {
                temp[j] = -curve_points[i - 1, j] + curve_points[i + 1, j];
            }

            temp_norm = Norm(temp);

            for (int j = 0; j < 3; j++) {
                curve_directions[i, j] = temp[j] / temp_norm;
                //Console.WriteLine(curve_directions[i, j]);
            }
        }

        float[,] bezier_points = new float[vertex * 4, 3];

        // Calculate control points and plot bezier curve segments
        for (int i = 0; i < vertex; i++) {
            // Compute curve_points{i + 1} - curve_points{i}
            for (int j = 0; j < 3; j++) {
                temp[j] = curve_points[i + 1, j] - curve_points[i, j];
            }

            float theta = Arg(curve_directions, i) - Arg(temp);
            float phi = Arg(temp) - Arg(curve_directions, i + 1);
            //Console.WriteLine(theta);
            //Console.WriteLine(phi);
            //Console.WriteLine("");

            float[] RhoSigma = new float[2];
            RhoSigma = VelocityParameters(theta, phi);

            float rho = RhoSigma[0];
            float sigma = RhoSigma[1];

            //Console.WriteLine(rho);
            //Console.WriteLine(sigma);
            //Console.WriteLine();


            // First control point
            for (int j = 0; j < 3; j++) {
                bezier_points[i * 4, j] = curve_points[i, j];
            }

            // Second control point
            // Compute curve_points{i + 1} - curve_points{i}
            for (int j = 0; j < 3; j++) {
                temp[j] = curve_points[i + 1, j] - curve_points[i, j];
            }
            temp_norm = Norm(temp);

            //Console.WriteLine(curve_points[0]);
            //Console.WriteLine(curve_points[1]);
            //Console.WriteLine(curve_points[2]);

            for (int j = 0; j < 3; j++) {
                bezier_points[i * 4 + 1, j] =
                    curve_points[i, j]
                    + rho / (3 * output_tension[i]) * temp_norm
                    * curve_directions[i, j];
                //Console.WriteLine(bezier_points[1, j]);
            }

            // Third control point
            for (int j = 0; j < 3; j++) {
                bezier_points[i * 4 + 2, j] =
                    curve_points[i + 1, j]
                    - sigma / (3 * input_tension[i]) * temp_norm
                    * curve_directions[i + 1, j];
            }

            // Fourth control point
            for (int j = 0; j < 3; j++) {
                bezier_points[i * 4 + 3, j] = curve_points[i + 1, j];
            }


            // for (int k = 0; k < 4; k++) {
            //     Console.Write("Control point #");
            //     Console.Write(k);
            //     Console.Write(": (");

            //     for (int j = 0; j < 3; j++) {
            //         Console.Write(bezier_points[k, j]);
            //         if (j < 2) {
            //             Console.Write(", ");
            //         }
            //     }
            //     Console.WriteLine(")");
            // }
            // Console.WriteLine();
        }

        return bezier_points;
    }

    private void drawCurve() {
		Vector2[] points = new Vector2[vertex];
		// PathSegment[] points2 = new PathSegment[vertex];
    	curve.enabled = true;
    	// curve2.enabled = true;

    	for (int i = 0; i < vertex; i++) {
			points[i] = new Vector2(donuts[i].transform.position.x/radius/2,donuts[i].transform.position.y/radius/2);
    		// points2[i] = new PathSegment(
    		// 	new Vector2(donuts[i].transform.position.x/radius/2+0.1f,
    		// 				donuts[i].transform.position.y/radius/2-0.05f),
    		// 	new Vector2(donuts[i].transform.position.x/radius/2,
    		// 				donuts[i].transform.position.y/radius/2+0.05f),
    		// 	new Vector2(donuts[i].transform.position.x/radius/2-0.1f,
    		// 		        donuts[i].transform.position.y/radius/2-0.05f));
    	}

    	curve.settings.polyVertices = points;
    	// curve2.settings.pathSegments = points2;


        // Compute the array of vertices the bezier curve should pass through
        Vector2[] pointsBezierShouldPassThrough = new Vector2[vertex];
        for (int i = 0; i < vertex; i++) {
            pointsBezierShouldPassThrough[i] =
                new Vector2(donuts[i].transform.position.x/radius/2,
                            donuts[i].transform.position.y/radius/2);
        }

        // Compute control points for the bezier curve
        float[,] bezier_points;
        bezier_points = ComputeBezierPassingThrough(pointsBezierShouldPassThrough);

        // Debug.Log(vertex);
        // Debug.Log(bezier_points.GetUpperBound(0));
        // Debug.Log(bezier_points.GetUpperBound(1));

        // Set control points for the bezier curve
        bezier.curveCount = vertex;
        bezier.controlPoints = new Vector3[vertex * 4];
        Debug.Assert(bezier.controlPoints.Length == bezier_points.GetUpperBound(0) + 1);
        for (int i = 0; i < bezier.controlPoints.Length; i++) {
            bezier.controlPoints[i] = new Vector3(bezier_points[i, 0], bezier_points[i, 1], bezier_points[i, 2]);
        }

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

        Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}
