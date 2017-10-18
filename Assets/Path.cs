using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    List<Vector3> m_points;
    Vector3 m_endPoint;

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
        for (int i = 0; i + 1 < m_points.Count; i++)
            Debug.DrawLine(m_points[i], m_points[i + 1], Color.Lerp(Color.green, Color.red, i / m_points.Count));
        Debug.DrawLine(m_points[m_points.Count - 1], m_endPoint, Color.red);
    }
}
