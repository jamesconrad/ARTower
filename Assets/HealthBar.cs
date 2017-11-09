using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public GameHandler game;
    public UnityEngine.UI.RawImage image;
    public RectTransform rectTransform;
    public float baseWidth;
    public float baseHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float width = baseWidth * (game.curHealth / game.maxHealth);
        //image.rectTransform.localScale = new Vector3(Mathf.Clamp(width, 0f, baseWidth), rectTransform.localScale.y, rectTransform.localScale.z);
        rectTransform.sizeDelta = new Vector2(Mathf.Clamp(width, 0f, baseWidth), baseHeight);
    }
}
