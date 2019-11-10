using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : IMeshPrimative
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

	// interpolates through the points array 0 <= t <= 1
	public Vector3 InterpolateAlongPoints(float t)
	{
		float tl = (points.Count - 1) * t;
		int PrevNode = Mathf.FloorToInt(tl);
		int NextNode = Mathf.CeilToInt(tl);
		if (NextNode >= points.Count) return points[points.Count - 1];
		if (PrevNode < 0) return points[0];
		return Vector3.Lerp(points[PrevNode], points[NextNode], tl - PrevNode);
	}

	public float InterpolateAlongWidths(float t)
	{
		float tl = (widths.Count - 1) * t;
		int PrevNode = Mathf.FloorToInt(tl);
		int NextNode = Mathf.CeilToInt(tl);
		if (NextNode >= widths.Count) return widths[widths.Count - 1];
		if (PrevNode < 0) return widths[0];
		return Mathf.Lerp(widths[PrevNode], widths[NextNode], tl - PrevNode);
	}

	public MeshData GetMeshData()
	{
		Noodle branch = new Noodle(InterpolateAlongPoints, InterpolateAlongWidths, points.Count);
		return branch.GetMeshData();
	}
}
