using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CurvedCylinder : IMeshPrimative 
{
	// uses input values 0 <= t <= 1 to calculate the curve postion and width at every point
	Func<float, Vector3> curveShapeFunc;
	Func<float, float> widthFunc;

	int resolution;

    public CurvedCylinder(Func<float, Vector3> curveShapeFunc, Func<float, float> widthFunc, int resolution = 20)
    {
        this.curveShapeFunc = curveShapeFunc;
        this.widthFunc = widthFunc;
        this.resolution = resolution;
    }

	public MeshData GetMeshData()
	{
		List<Vector3> points = new List<Vector3>();
		List<float> widths = new List<float>();
		for (int i = 0; i <= resolution; i++)
		{
			float t = i / (float)resolution;
			points.Add(curveShapeFunc(t));
			widths.Add(widthFunc(t));
		}

		for (int i = 0; i <= resolution; i++)
		{
			
		}
	}
}
