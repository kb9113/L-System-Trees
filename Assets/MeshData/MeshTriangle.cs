using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class used to store triangles in meshData
public class MeshTriangle 
{
	// the 3 vertexes of the triangle
	public Vector3 v1;
	public Vector3 v2;
	public Vector3 v3;

	// the group the triangle is in used when calcualting normals
	int triangleGroup;

    public MeshTriangle(Vector3 v1, Vector3 v2, Vector3 v3, int triangleGroup)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
        this.triangleGroup = triangleGroup;
    }
}
