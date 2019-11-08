using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch 
{
	public List<Vector3> points;
	public List<float> widths;
	public List<Branch> children;

	public Branch()
	{
		points = new List<Vector3>();
		widths = new List<float>();
		children = new List<Branch>();
	}

	public Branch(Vector3 point, float width)
	{
		points = new List<Vector3>() { point };
		widths = new List<float>() { width };
		children = new List<Branch>();
	}

	public int Count
	{
		get 
		{
			return points.Count;
		}
	}

	public void AddAfter(Vector3 point, float width)
	{
		points.Add(point);
		widths.Add(width);
	}

	public void AddBefore(Vector3 point, float width)
	{
		points.Insert(0, point);
		widths.Insert(0, width);
	}

	public void Taper()
	{
		int count = widths.Count;
		if (count > 0) widths[count - 1] = 0;
	}

	public void AddChild(Branch child)
	{
		children.Add(child);
	}

	public MeshData GetMeshData()
	{
		return null;
	}
}
