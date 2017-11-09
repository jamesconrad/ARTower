using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveNumberDisplay : MonoBehaviour {


    public GameHandler game;
    public UnityEngine.UI.Text text;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Wave: " + game.m_waveNumber.ToString();
	}
}
