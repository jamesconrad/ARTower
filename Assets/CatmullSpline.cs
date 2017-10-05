using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullSpline : MonoBehaviour {

    public Vector3[] points;
    public Vector3[] path;
    
    int ClampListPos(int pos)
    {
        if (pos < 0)
            pos = points.Length - 1;

        if (pos > points.Length)
            pos = 1;
        else if (pos > points.Length - 1)
            pos = 0;

        return pos;
    }

    //Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
    //http://www.iquilezles.org/www/articles/minispline/minispline.htm
    Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        //The cubic a + b*t + c*t^2 + d*t^3
        //0.5 coefficent saved until now
        Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return pos;
    }

    public Vector3 GetCatmullRomPosition(float t)
    {
        Vector3 a = 2f * points[1];
        Vector3 b = points[2] - points[0];
        Vector3 c = 2f * points[0] - 5f * points[1] + 4f * points[2] - points[3];
        Vector3 d = -points[0] + 3f * points[1] - 3f * points[2] + points[3];

        //The cubic a + b*t + c*t^2 + d*t^3
        //0.5 coefficent saved until now
        Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return pos;
    }

    //some thoughts,
    //using linear interpolation for point approximation on line via projection then
    //using that t value to determine stage
    //and travelling towards the actual point a few steps beyond that

    public void SetCapacity(int length)
    {
        points = new Vector3[length];
    }

    public int GetLength()
    {
        return points.Length;
    }
}
