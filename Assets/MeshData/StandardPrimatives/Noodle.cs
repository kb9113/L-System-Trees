using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Noodle : IMeshPrimative 
{
	// uses input values 0 <= t <= 1 to calculate the curve postion and width at every point
	Func<float, Vector3> curveShapeFunc;
	Func<float, float> widthFunc;

	int resolution;

    public Noodle(Func<float, Vector3> curveShapeFunc, Func<float, float> widthFunc, int resolution = 20)
    {
        this.curveShapeFunc = curveShapeFunc;
        this.widthFunc = widthFunc;
        this.resolution = resolution;
    }

	public MeshData GetMeshData()
	{
		// generate points on curve
		List<Vector3> points = new List<Vector3>();
		List<float> widths = new List<float>();
		for (int i = 0; i <= resolution; i++)
		{
			float t = i / (float)resolution;
			points.Add(curveShapeFunc(t));
			widths.Add(widthFunc(t));
		}

		// generate points
		Vector3 tangent = points[1] - points[0];
		Vector3 xNormal = Vector3.Cross(tangent, Vector3.up);
		if (xNormal.magnitude < 0.01f)
		{
			// if the magnitude is to small they point in too simlar of a direction
			xNormal = Vector3.Cross(tangent, Vector3.left);
		}
		xNormal = xNormal.normalized;
		Vector3 yNormal = Vector3.Cross(tangent, xNormal).normalized;
		Vector3[,] verts = new Vector3[resolution + 1, resolution];
		for (int i = 0; i <= resolution; i++)
		{
			if (i != 0)
			{
				// recalculate the normal
				tangent = points[i] - points[i - 1];
				xNormal = Vector3.Cross(yNormal, tangent).normalized;
				yNormal = Vector3.Cross(tangent, xNormal).normalized;
			}
			for (int j = 0; j < resolution; j++)
			{
				float theta = Mathf.Lerp(0, 2 * Mathf.PI, j / (float)resolution);
				Vector3 v = widths[i] * (Mathf.Cos(theta) * xNormal + Mathf.Sin(theta) * yNormal);
				verts[i, j] = v + points[i];
			}
		}

		// create meshData
		MeshData md = new MeshData();
		for (int i = 0; i < resolution; i++)
		{
			for (int j = 0; j < resolution; j++)
			{
				Vector3 v1 = verts[i, j];
				Vector3 v2 = verts[i, (j + 1) % resolution];
				Vector3 v3 = verts[i + 1, (j + 1) % resolution];
				Vector3 v4 = verts[i + 1, j];
				md.AddQuad(v1, v2, v3, v4);
			}
		}
		return md;
	}
}
