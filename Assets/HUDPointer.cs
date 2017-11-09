using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPointer : MonoBehaviour {
    
    /*
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
                                                                               //transform.position = onScreenPos;


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
    

    public UnityEngine.UI.Image icon; //The icon. Preferably an arrow pointing upwards.
    public float iconSize = 50f;
    [HideInInspector]
    public GUIStyle gooey; //GUIStyle to make the box around the icon invisible. Public so that everything has the default stats.
    Vector2 indRange;
    Camera cam;
    float scaleRes = Screen.width / 500; //The width of the screen divided by 500. Will make the GUI automatically
                                         //scale with varying resolutions.
    bool visible = true; //Whether or not the object is visible in the camera.

    bool tracked;
    Transform tracking;
    public string trackingTag;

    void Start()
    {
        visible = GetComponent<SpriteRenderer>().isVisible;

        cam = Camera.main; //Don't use Camera.main in a looping method, its very slow, as Camera.main actually
                           //does a GameObject.Find for an object tagged with MainCamera.

        indRange.x = Screen.width - (Screen.width / 6);
        indRange.y = Screen.height - (Screen.height / 7);
        indRange /= 2f;

        tracked = false;

        gooey.normal.textColor = new Vector4(0, 0, 0, 0); //Makes the box around the icon invisible.
    }

    void OnGUI()
    {
        if (!tracked)
        {
            if (GameObject.FindGameObjectsWithTag(trackingTag).Length > 0)
            {
                tracked = true;
                tracking = GameObject.FindGameObjectsWithTag(trackingTag)[0].transform;
            }
        }

        if (!visible)
        {
            Vector3 dir = transform.position - cam.transform.position;
            dir = Vector3.Normalize(dir);
            dir.y *= -1f;

            Vector2 indPos = new Vector2(indRange.x * dir.x, indRange.y * dir.y);
            indPos = new Vector2((Screen.width / 2) + indPos.x,
                              (Screen.height / 2) + indPos.y);

            Vector3 pdir = transform.position - cam.ScreenToWorldPoint(new Vector3(indPos.x, indPos.y,
                                                                                    transform.position.z));
            pdir = Vector3.Normalize(pdir);

            float angle = Mathf.Atan2(pdir.x, pdir.y) * Mathf.Rad2Deg;

            GUIUtility.RotateAroundPivot(angle, indPos); //Rotates the GUI. Only rotates GUI drawn after the rotate is called, not before.
            //GUI.Box(new Rect(indPos.x, indPos.y, scaleRes * iconSize, scaleRes * iconSize), icon);
            GUIUtility.RotateAroundPivot(0, indPos); //Rotates GUI back to the default so that GUI drawn after is not rotated.
        }
    }

    void OnBecameInvisible()
    {
        visible = false;
    }
    //Turns off the indicator if object is onscreen.
    void OnBecameVisible()
    {
        visible = true;
    }
    */
}
