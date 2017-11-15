using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttatchOnAwake : MonoBehaviour {

    public string targetTag;
    public GameObject attach;

    private void Awake()
    {
        Instantiate(attach, GameObject.FindGameObjectWithTag(targetTag).transform);
    }
}
