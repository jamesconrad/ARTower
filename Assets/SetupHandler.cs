﻿namespace GoogleARCore.HelloAR
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Rendering;
    using GoogleARCore;
    public class SetupHandler : MonoBehaviour
    {
        private List<TrackedPlane> m_newPlanes = new List<TrackedPlane>();
        private List<TrackedPlane> m_allPlanes = new List<TrackedPlane>();
        public GameObject m_trackedPlanePrefab;
        public Camera m_firstPersonCamera;
        public GameObject m_slimeCave;
        public GameObject m_homeBase;
        public GameObject m_flag;
        public UnityEngine.UI.Button m_beginButton;
        private Path m_path;

        public GameHandler m_gameHandler;

        public UnityEngine.UI.Text text;
        public GameObject notification;

        int state = 0;

        // Use this for initialization
        void Start()
        {
            m_path = gameObject.AddComponent<Path>();
            m_path.lineRenderer = gameObject.GetComponent<LineRenderer>();
            m_beginButton.onClick.AddListener(BeginButtonPressed);
        }

        // Update is called once per frame
        void Update()
        {
            Frame.GetNewPlanes(ref m_newPlanes);

            // Iterate over planes found in this frame and instantiate corresponding GameObjects to visualize them.
            for (int i = 0; i < m_newPlanes.Count; i++)
            {
                // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
                // the origin with an identity rotation since the mesh for our prefab is updated in Unity World
                // coordinates.
                GameObject planeObject = Instantiate(m_trackedPlanePrefab, Vector3.zero, Quaternion.identity,
                    transform.parent);
                planeObject.GetComponent<TrackedPlaneVisualizer>().SetTrackedPlane(m_newPlanes[i]);

                // Apply a random color and grid rotation.
                planeObject.GetComponent<Renderer>().material.SetColor("_GridColor", new Color(0.956f, 0.262f, 0.211f));
                planeObject.GetComponent<Renderer>().material.SetFloat("_UvRotation", Random.Range(0.0f, 360.0f));

                if (text.text == "Searching for a plane.")
                    text.text = "Tap a start point.";
            }
            
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            TrackableHit hit;
            TrackableHitFlag raycastFilter = TrackableHitFlag.PlaneWithinBounds | TrackableHitFlag.PlaneWithinPolygon;

            if (Session.Raycast(m_firstPersonCamera.ScreenPointToRay(touch.position), raycastFilter, out hit))
            {
                // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                // world evolves.
                var anchor = Session.CreateAnchor(hit.Point, Quaternion.identity);

                // Intanstiate an object as a child of the anchor; it's transform will now benefit
                // from the anchor's tracking.
                GameObject newObject;
                if (state == 0)
                {
                    newObject = Instantiate(m_slimeCave, hit.Point, Quaternion.identity, anchor.transform);
                    m_path.AddPoint(newObject.transform.position);
                    text.text = "Tap an end point.";
                }
                else if (state == 1)
                {
                    newObject = Instantiate(m_homeBase, hit.Point, Quaternion.identity, anchor.transform);
                    m_path.AddEndPoint(newObject.transform.position);
                    text.text = "Tap to add points to the path.";
                }
                else
                {
                    newObject = Instantiate(m_flag, hit.Point, Quaternion.identity, anchor.transform);
                    m_path.InsertPoint(newObject.transform.position);
                }

                m_path.BuildCurve();

                newObject.transform.LookAt(m_firstPersonCamera.transform);
                newObject.transform.rotation = Quaternion.Euler(0.0f,
                    newObject.transform.rotation.eulerAngles.y, newObject.transform.rotation.z);
                // Use a plane attachment component to maintain Andy's y-offset from the plane
                // (occurs after anchor updates).
                newObject.GetComponent<PlaneAttachment>().Attach(hit.Plane);
                state++;
                if (state >= 2)
                {
                    m_beginButton.interactable = true;
                    m_beginButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Begin Game.";
                    m_beginButton.GetComponent<RectTransform>().localPosition = new Vector3(0, -150, 0);
                }
            }
        }

        void BeginButtonPressed()
        {
            if (state >= 2)
            {
                m_beginButton.interactable = true;
                m_beginButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "";
                m_beginButton.GetComponent<RectTransform>().localPosition = new Vector3(-1000, 1000, 0);
                notification.SetActive(false);
                m_gameHandler.SetPath(m_path);
                m_gameHandler.enabled = true;
                m_gameHandler.Prep();
                this.enabled = false;
            }
        }
    }
}
