using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachOnAwake : MonoBehaviour {

    public string targetTag;
    public GameObject attach;

    private void Awake()
    {

        GameObject go = GameObject.FindGameObjectWithTag(targetTag);
        GameObject ngo = Instantiate(attach, go.transform);
        ngo.transform.position = new Vector3(ngo.transform.position.x, ngo.transform.position.y + 0.65f, ngo.transform.position.z);
        ngo.SetActive(true);
    }
}
