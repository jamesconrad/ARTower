﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour {

    /*
    public class SlimeJSON
    {
        float colourr, colourg, colourb;
        float scalex, scaley, scalez;
        float velocity;
        int numsubslimes;
        int[] subslimes;//slimes that are after it
    }
    */
    public GameObject deathParticles;

    //private static bool initialized = false;
    public static Transform baseTransform;
    public static Transform spawnTransform;

    Material material;
    int numsubslimes;
    int[] subslimes;
    float velocity;

    public int damage;
    

    private float lerpt;

    public void ApplySlimeJSON(GameHandler.SlimeJSON s)
    {
        material = GetComponent<Renderer>().material;
        subslimes = new int[s.subslimes.Length];
        for (int i = 0; i < subslimes.Length; i++)
            subslimes[i] = s.subslimes[i];
        
        material.color = new Color(s.colourr, s.colourg, s.colourb);
        transform.localScale = new Vector3(s.scalex, s.scaley, s.scalez);
        numsubslimes = s.numsubslimes;
        damage = s.damage;
        velocity = s.velocity;
    }

    public void UpdateTargetPositions(Transform _baseTransform, Transform _spawnTransform)
    {
        baseTransform = _baseTransform;
        spawnTransform = _spawnTransform;
    }

	// Use this for initialization
	void Start ()
    {
        lerpt = 0;
	}

	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(spawnTransform.position, baseTransform.position, lerpt);
        lerpt += (float)(Time.deltaTime * 0.1) * velocity;
        //move towards the thing.
	}

    public void OnHit()
    {
        OnDeath();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Base")
            OnHitBase();
    }

    public void OnHitBase()
    {
        //Just do particles
        GameObject go;
        go = Instantiate(deathParticles, transform.position, transform.rotation) as GameObject;
        go.transform.parent = transform;
        Destroy(gameObject);
    }

    void OnDeath()
    {
        //spawn subslimes by prefab
        for (int i = 0; i < numsubslimes; i++)
            transform.parent.GetComponent<GameHandler>().SpawnSlime(subslimes[i]);
        //play death particles
        GameObject go;
        go = Instantiate(deathParticles, transform.position, Quaternion.Euler(-90.0f,0.0f,0.0f)) as GameObject;
        go.transform.parent = transform.parent;
        go.transform.localScale = transform.localScale;
        ParticleSystem.ColorOverLifetimeModule m = go.GetComponent<ParticleSystem>().colorOverLifetime;
        Gradient g = new Gradient();
        g.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(material.color, 0.0f),
                new GradientColorKey(material.color, 1.0f)}, 
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(1.0f, 0.75f),
                new GradientAlphaKey(0.0f, 1.0f) });
        m.enabled = true;
        m.color = g;
    }
}
