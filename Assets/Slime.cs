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
    public static Transform baseTransform;
    public static Transform spawnTransform;

    Material material;
    int numsubslimes;
    int[] subslimes;
    float velocity;

    Path path;
    public int linesegment;
    public float lerpt;

    public int damage;
    


    public void ApplySlimeJSON(GameHandler.SlimeJSON s, Path p)
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
        path = p;
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
        transform.position = path.GetPosition(linesegment, lerpt);
        lerpt += (float)(Time.deltaTime) * velocity;
        if (lerpt >= 1)
        {
            lerpt = 0;
            linesegment++;
        }
        //move towards the thing.
	}

    public void OnHit()
    {
        print("Hit");
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
        transform.parent.GetComponent<GameHandler>().curHealth -= damage;
        Destroy(gameObject);
    }

    void OnDeath()
    {
        //spawn subslimes by prefab
        for (int i = 0; i < numsubslimes; i++)
            transform.parent.GetComponent<GameHandler>().SpawnSlime(subslimes[i],linesegment,lerpt);
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
