using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public GameHandler game;
    public UnityEngine.UI.RawImage image;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        image.transform.localScale = new Vector3(Mathf.Clamp(game.maxHealth / game.curHealth, 0f, 1f),
                                   image.transform.localScale.y,
                                   image.transform.localScale.z);
    }
}
