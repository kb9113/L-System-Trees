using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshData 
{
	// stores all vertexes and which triangles they are part of
	public Dictionary<Vector3, List<MeshTriangle>> verts = new Dictionary<Vector3, List<MeshTriangle>>();
	
	void createVertex(Vector3 position)
	{
		verts.Add(position, new List<MeshTriangle>());
	}

	public void AddTriangle(Vector3 a, Vector3 b, Vector3 c, int group = 0)
	{
		MeshTriangle tri = new MeshTriangle(a, b, c, group);
		AddTriangle(tri);
	}

	public void AddQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int group = 0)
	{
		MeshTriangle tri1 = new MeshTriangle(a, b, c, group);
		MeshTriangle tri2 = new MeshTriangle(a, c, d, group);
		AddTriangle(tri1);
		AddTriangle(tri2);
	}

	void AddTriangle(MeshTriangle tri)
	{
		if (tri.v1 == tri.v2 || tri.v2 == tri.v3 || tri.v1 == tri.v3) return;
		if (!verts.ContainsKey(tri.v1)) createVertex(tri.v1);
		if (!verts.ContainsKey(tri.v2)) createVertex(tri.v2);
		if (!verts.ContainsKey(tri.v3)) createVertex(tri.v3);
		verts[tri.v1].Add(tri);
		verts[tri.v2].Add(tri);
		verts[tri.v3].Add(tri);
	}

	public List<MeshTriangle> GetTriangles()
	{	
		// construct a hasSet of all triangles
		HashSet<MeshTriangle> triangles = new HashSet<MeshTriangle>();
		foreach (List<MeshTriangle> tris in verts.Values)
		{
			foreach(MeshTriangle tri in tris)
			{
				triangles.Add(tri);
			}
		}
		return triangles.ToList();
	}

	// adds md to this
	public void AddMesh(MeshData md)
	{
		List<MeshTriangle> triangles = md.GetTriangles();
		// add the triangles to this meshdata
		foreach (MeshTriangle tri in triangles)
		{
			AddTriangle(tri);
		}
	}

	public void AddPrimative(IMeshPrimative primative)
	{
		AddMesh(primative.GetMeshData());
	}

	public Mesh GetMesh()
	{
		// first generate a vertex to index map
		Dictionary<Vector3, int> vertToIndex = new Dictionary<Vector3, int>();
		List<Vector3> vertsList = new List<Vector3>();
		int i = 0;
		foreach (Vector3 v in verts.Keys)
		{
			vertToIndex.Add(v, i);
			vertsList.Add(v);
			i++;
		}

		List<MeshTriangle> triangles = GetTriangles();
		List<int> trianglesList = new List<int>();
		foreach (MeshTriangle tri in triangles)
		{
			trianglesList.Add(vertToIndex[tri.v1]);
			trianglesList.Add(vertToIndex[tri.v2]);
			trianglesList.Add(vertToIndex[tri.v3]);
		}

		Mesh mesh = new Mesh();
		mesh.vertices = vertsList.ToArray();
		mesh.triangles = trianglesList.ToArray();
		mesh.RecalculateNormals();
		return mesh;
	}
}
