using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    List<Vector3> m_points;
    Vector3 m_endPoint;
    public LineRenderer lineRenderer;

    private void Start()
    {
        m_points = new List<Vector3>();
    }

    public int Count()
    {
        return m_points.Count;
    }

    public Vector3 Point(int index)
    {
        return m_points[index];
    }

    public void AddPoint(Vector3 p)
    {
        m_points.Add(p);
    }

    public void InsertPoint(Vector3 p)
    {
        m_points.Insert(m_points.Count - 1, p);
    }

    public void AddEndPoint(Vector3 p)
    {
        m_endPoint = p;
        m_points.Add(p);
    }

    public void BuildCurve()
    {
        lineRenderer.positionCount = m_points.Count;
        lineRenderer.SetPositions(m_points.ToArray());
        //...
    }

    public Vector3 GetPosition(int linesegment, float tval)
    {
        return Vector3.Lerp(m_points[linesegment], m_points[linesegment + 1], tval);
    }
}
