using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPointer : MonoBehaviour {

    public Camera cam;
    public string trackingTag;
    public UnityEngine.UI.RawImage image;
    bool tracked;
    Transform tracking;
    Vector2 screenPos;
    Vector2 onScreenPos;
    float max;


    void Start()
    {
        cam = Camera.main;
        tracked = false;
    }

    void Update()
    {
        if (!tracked)
        {
            if (GameObject.FindGameObjectsWithTag(trackingTag).Length > 0)
            {
                tracked = true;
                tracking = GameObject.FindGameObjectsWithTag(trackingTag)[0].transform;
            }
        }

        screenPos = cam.WorldToViewportPoint(tracking.position); //get viewport positions
        float alpha = image.color.a;

        if (screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            return;
        }
        else
        {
            onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2; //2D version, new mapping
            max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
            onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping
            transform.position = onScreenPos;
            print(onScreenPos);


            image.color = new Color(image.color.r, image.color.g, image.color.b, onScreenPos.magnitude);
            float angle = AngleBetweenVector2(Vector2.right, onScreenPos);
            
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
