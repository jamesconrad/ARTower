using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    List<Vector3> m_points;
    Vector3 m_endPoint;
    GLDebug m_glDebug;

    private void Start()
    {
        m_points = new List<Vector3>();
        m_glDebug = gameObject.AddComponent<GLDebug>();
    }

    public void AddPoint(Vector3 p)
    {
        m_points.Add(p);
    }

    public void AddEndPoint(Vector3 p)
    {
        m_endPoint = p;
    }

    public void BuildCurve()
    {
        m_points.Add(m_endPoint);
        //...
    }

    public void DrawLinearCurve()
    {
        if (m_points.Count >= 2)
        {
            for (int i = 0; i + 1 < m_points.Count; i++)
                GLDebug.DrawLine(m_points[i], m_points[i + 1], Color.Lerp(Color.green, Color.red, i / m_points.Count));
            GLDebug.DrawLine(m_points[m_points.Count - 1], m_endPoint, Color.red);
        }
    }
}
