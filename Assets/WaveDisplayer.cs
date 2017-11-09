﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDisplayer : MonoBehaviour {
    
    public GameHandler game;
    public GameObject baseSlimeDisplay;
    public float offset;

    private int displayedWave;

	// Update is called once per frame
	void LateUpdate ()
    {
        if (!game.isActiveAndEnabled)
            return;

        if (displayedWave != game.m_waveNumber + 1)
        {
            //purge children
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            displayedWave = game.m_waveNumber;

            if (displayedWave > game.m_waves.Count)
                return;

            for (int i = 0; i < game.m_waves[displayedWave].slimes.Length; i++)
            {
                GameHandler.SlimeJSON slimeJSON = game.m_slimes[game.m_waves[displayedWave].slimes[i]];
                //spawn child
                GameObject slime = Instantiate(baseSlimeDisplay, transform) as GameObject;
                slime.transform.position = new Vector3(transform.position.x, transform.position.y + i * offset, transform.position.z);
                slime.GetComponent<UnityEngine.UI.RawImage>().color = new Color(slimeJSON.colourr, slimeJSON.colourg, slimeJSON.colourb);
                slime.GetComponentInChildren<UnityEngine.UI.Text>().text = "x" + game.m_waves[displayedWave].counts[i].ToString();
            }
        }
	}
}
