using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    
    private Transform target;

    private void Awake()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.LookAt(target.position);	
	}
}
