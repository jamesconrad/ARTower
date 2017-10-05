using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathManager : MonoBehaviour {

    public CatmullSpline cspline;

	// Use this for initialization
	void Start () {
        cspline = gameObject.AddComponent<CatmullSpline>();
        cspline.SetCapacity(4);
        cspline.points[1] = new Vector3(-3.75f, 0.5f, 3.75f);
        cspline.points[2] = new Vector3(3.75f, 0.5f, 3.75f);
        cspline.points[3] = new Vector3(3.75f, 0.5f, -3.75f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
