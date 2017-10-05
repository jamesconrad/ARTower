using System.Collections;
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
    private static Transform baseTransform;
    private static Transform spawnTransform;

    Material material;
    int numsubslimes;
    int[] subslimes;
    public int damage;

    public void ApplySlimeJSON(GameHandler.SlimeJSON s)
    {
        subslimes = new int[s.subslimes.Length];
        for (int i = 0; i < subslimes.Length; i++)
            subslimes[i] = s.subslimes[i];
        material.color = new Color(s.colourr, s.colourg, s.colourb);
        transform.localScale = new Vector3(s.scalex, s.scaley, s.scalez);
        numsubslimes = s.numsubslimes;
        damage = s.damage;
    }

    public void UpdateTargetPositions(Transform _baseTransform, Transform _spawnTransform)
    {
        baseTransform = _baseTransform;
        spawnTransform = _spawnTransform;
    }

	// Use this for initialization
	void Start ()
    {
        
	}

	// Update is called once per frame
	void Update ()
    {
        //move towards the thing.
	}

    public void OnHit()
    {
        OnDeath();
        Destroy(gameObject);
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
        go = Instantiate(deathParticles, transform.position, transform.rotation) as GameObject;
        go.transform.parent = transform;
    }
}
