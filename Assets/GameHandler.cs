using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public GameObject slimeBase;
    public Camera m_camera;


    GameObject m_base;
    GameObject m_spawn;

    int m_waveNumber = 0;

    List<WaveJSON> m_waves;
    List<SlimeJSON> m_slimes;

    public UnityEngine.UI.Text text;

    public TextAsset slimestxt;
    public TextAsset wavestxt;

    float[] m_spawnDelays;
    int[] m_spawnsLeft;
    bool m_waveComplete = true;



    [System.Serializable]
    public class WaveJSON
    {
                                //for each index the following happens:
        public int[] slimes;    //slime to spawn
        public float[] delays;  //delay from start to spawn it, repeats
        public int[] counts;    //number to spawn in the wave
    }

    [System.Serializable]
    public class SlimeJSON
    {
        public float colourr, colourg, colourb;
        public float scalex, scaley, scalez;
        public float velocity;
        public int numsubslimes;
        public int[] subslimes;//slimes that are after it
        public int damage;
    }

    // Use this for initialization
    void Start () {
        //script starts disabled and will be toggling a fair bit
	}

    public void Prep()
    {
        m_waves = new List<WaveJSON>();
        m_slimes = new List<SlimeJSON>();
        m_spawn = GameObject.FindGameObjectWithTag("Spawn");
        m_base = GameObject.FindGameObjectWithTag("Base");
        Slime.spawnTransform = m_spawn.transform;
        Slime.baseTransform = m_base.transform;
        print(slimestxt.ToString());
        print(wavestxt.ToString());
        //prep the paths
        GameObject.FindGameObjectsWithTag("Base");
        GameObject.FindGameObjectsWithTag("Spawn");
        PrepWaves();
        PrepSlimes();
        print("Done Prep");
    }

    // Update is called once per frame
    void Update ()
    {
        
        //check if wave is over
        if (m_waveComplete && transform.childCount <= 0)
        {
            //reset
            m_waveComplete = false;
            m_spawnDelays = new float[m_waves[m_waveNumber].delays.Length];
            m_spawnsLeft = new int[m_waves[m_waveNumber].counts.Length];
            //refill variables
            for (int i = 0; i < m_waves[m_waveNumber].delays.Length; i++)
            {
                m_spawnDelays[i] = m_waves[m_waveNumber].delays[i];
                m_spawnsLeft[i] = m_waves[m_waveNumber].counts[i];
            }
        }
        


        //wave spawning
        bool slimesRemain = false;
        for (int i = 0; i < m_spawnDelays.Length; i++)
        {
            if (m_spawnsLeft[i] > 0)
                slimesRemain = true;
            //check adjust delay
            m_spawnDelays[i] -= Time.deltaTime;
            //check if we are ready to spawn, and if we have any left
            if (m_spawnDelays[i] <= 0 && m_spawnsLeft[i] > 0)
            {
                //spawn a new slime
                m_spawnDelays[i] = m_waves[m_waveNumber].delays[i];
                m_spawnsLeft[i]--;
                SpawnSlime(m_waves[m_waveNumber].slimes[i]);
            }
        }
        if (slimesRemain == false)
        {
            m_waveNumber++;
            m_waveComplete = true;
        }

        //bottom left corner wave/debugging info
        string childInfo = "Total Children: " + transform.childCount;
        for (int i = 0; i < transform.childCount; i++)
            childInfo += "\nChild: " + i + "\tName: " + transform.GetChild(i).name;
        string waveInfo = "Wave: " + m_waveNumber + "\tWave Complete: " + m_waveComplete + "\nslime\t\tdelays\t\tspawns\n";
        for (int i = 0; i < m_waves[m_waveNumber].delays.Length; i++)
            waveInfo += m_waves[m_waveNumber].slimes[i] + "\t\t\t\t" + m_spawnDelays[i] + "\t" + m_spawnsLeft[i] + "\n";
        text.text = childInfo + "\n" + waveInfo;

        //check for hits
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // dir camera.ScreenPointToRay(touch.position)
        RaycastHit hit;
        if (Physics.Raycast(m_camera.ScreenPointToRay(new Vector3(0, 0, 0)), out hit, 1024.0f) && hit.transform.tag == "Slime")
        {
            hit.collider.gameObject.GetComponent<Slime>().OnHit();
        }
    }

    void PrepWaves()
    {
        //FileAssist file = new FileAssist();
        //file.OpenFile(System.IO.Path.Combine(Application.streamingAssetsPath, "waves.txt"), true);
        string[] fbl = wavestxt.ToString().Split('\n');
        //List<string> fbl = file.ReadAllToMemory();
        for (int i = 0; i < fbl.Length; i++)
            m_waves.Add(JsonUtility.FromJson<WaveJSON>(fbl[i]));
    }

    void PrepSlimes()
    {
        //FileAssist file = new FileAssist();
        //file.OpenFile(System.IO.Path.Combine(Application.streamingAssetsPath, "slimes.txt"), true);
        string[] fbl = slimestxt.ToString().Split('\n');
        //List<string> fbl = file.ReadAllToMemory();
        for (int i = 0; i < fbl.Length; i++)
            m_slimes.Add(JsonUtility.FromJson<SlimeJSON>(fbl[i]));
    }

    public void SpawnSlime(int id)
    {
        GameObject go;
        go = Instantiate(slimeBase, m_spawn.transform.position, m_spawn.transform.rotation) as GameObject;
        go.transform.parent = transform;
        go.GetComponent<Slime>().ApplySlimeJSON(m_slimes[id]);
    }
}
