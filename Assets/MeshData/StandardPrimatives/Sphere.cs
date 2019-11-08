using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : IMeshPrimative 
{
	float radius;
	int resolution;

    public Sphere(float radius, int resolution = 100)
    {
        this.radius = radius;
		this.resolution = resolution;
    }

	public MeshData GetMeshData()
	{
		Vector3[,] spherePoints = new Vector3[resolution + 1, resolution];
		for (int i = 0; i <= resolution; i++)
		{
			float ti = Mathf.InverseLerp(0, resolution, i);
			float lat = Mathf.Lerp(0, Mathf.PI, ti);
			
			for (int j = 0; j < resolution; j++)
			{
				float tj = Mathf.InverseLerp(0, resolution, j);
				float lon = Mathf.Lerp(0, 2 * Mathf.PI, tj);

				float x = radius * Mathf.Sin(lat) * Mathf.Cos(lon);
				float y = radius * Mathf.Sin(lat) * Mathf.Sin(lon);
				float z = radius * Mathf.Cos(lat);
				spherePoints[i, j] = new Vector3(x, y, z);
			}
		}

		MeshData md = new MeshData();
		for (int i = 0; i < resolution; i++)
		{
			for (int j = 0; j < resolution; j++)
			{
				Vector3 v1 = spherePoints[i, j];
				Vector3 v2 = spherePoints[i + 1, j];
				Vector3 v3 = spherePoints[i + 1, (j + 1) % resolution];
				Vector3 v4 = spherePoints[i, (j + 1) % resolution];
				md.AddQuad(v1, v2, v3, v4);
			}
		}
		return md;
	}
}
