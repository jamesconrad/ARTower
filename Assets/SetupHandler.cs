namespace GoogleARCore.HelloAR
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

        public GameHandler m_gameHandler;

        public UnityEngine.UI.Text text;

        int state = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            print("hehe xD active btw");
            Frame.GetNewPlanes(ref m_newPlanes);

            // Iterate over planes found in this frame and instantiate corresponding GameObjects to visualize them.
            for (int i = 0; i < m_newPlanes.Count; i++)
            {
                // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
                // the origin with an identity rotation since the mesh for our prefab is updated in Unity World
                // coordinates.
                GameObject planeObject = Instantiate(m_trackedPlanePrefab, Vector3.zero, Quaternion.identity,
                    transform);
                planeObject.GetComponent<TrackedPlaneVisualizer>().SetTrackedPlane(m_newPlanes[i]);

                // Apply a random color and grid rotation.
                planeObject.GetComponent<Renderer>().material.SetColor("_GridColor", new Color(0.956f, 0.262f, 0.211f));
                planeObject.GetComponent<Renderer>().material.SetFloat("_UvRotation", Random.Range(0.0f, 360.0f));
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
                var newObject = Instantiate((state == 0 ? m_slimeCave : m_homeBase), hit.Point, Quaternion.identity,
                    anchor.transform);

                // Andy should look at the camera but still be flush with the plane.
                newObject.transform.LookAt(m_firstPersonCamera.transform);
                newObject.transform.rotation = Quaternion.Euler(0.0f,
                    newObject.transform.rotation.eulerAngles.y, newObject.transform.rotation.z);
                // Use a plane attachment component to maintain Andy's y-offset from the plane
                // (occurs after anchor updates).
                newObject.GetComponent<PlaneAttachment>().Attach(hit.Plane);
                state++;
                if (state >= 2)
                {
                    text.text = "Game ready.";
                    m_gameHandler.enabled = true;
                    this.enabled = false;
                }
            }
        }
    }
}
