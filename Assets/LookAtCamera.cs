using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    
    private Transform target;

    private void Awake()
    {
        print("Finding camera...");
        target = Camera.main.transform;
        print("Camera found." + target);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.rotation = target.rotation;
	}
}
