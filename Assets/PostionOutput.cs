using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostionOutput : MonoBehaviour {

    private TextMesh text;
    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        text = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = cam.transform.rotation;

        float scale = (cam.transform.position - transform.position).magnitude * 0.25f;
        transform.localScale = new Vector3(scale, scale, scale);

        //update text
        text.text = (transform.position - new Vector3(0, -0.2f, 0)).ToString();
    }
}
