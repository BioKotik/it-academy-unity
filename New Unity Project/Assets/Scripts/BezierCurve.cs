using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Transform point0, point1, point2;
    public int numPoints;
    private Vector3[] positions; 

    private void Start()
    {
        positions = new Vector3[numPoints];
       
    }

  public void DrawLinearCurve()
    {
        for (int i = 1; i < numPoints+1; i++)
        {
            float step = i / (float) numPoints;
            positions[i - 1] = CalculateLinearBezierPOint(step, point0.position, point1.position);
        }
        lineRenderer.SetPositions(positions);
    }
public void DrawQuadraticCurve()
    {
        for (int i = 1; i < numPoints+1; i++)
        {
            float step = i / (float) numPoints;
            positions[i - 1] = CalculateQuadraticBezierPOint(step, point0.position, point1.position, point2.position);
        }
        lineRenderer.SetPositions(positions);
    }

public Vector3 CalculateLinearBezierPOint(float step, Vector3 startPoint, Vector3 targetPoint)
    {
        return startPoint + step * (targetPoint - startPoint);
    }
    
public Vector3 CalculateQuadraticBezierPOint(float step, Vector3 startPoint, Vector3 midPoint, Vector3 targetPoint)
    {
        float progress = 1 - step;
        float stepPow2 = step * step;
        float progressPow2 = progress * progress;
        Vector3 p = progressPow2 * startPoint;
        p += 2 * progress * step * midPoint;
        p += stepPow2 * targetPoint;
        return p;
    }
}
