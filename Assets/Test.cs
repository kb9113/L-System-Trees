using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Test : MonoBehaviour 
{
	Tree tree;
	int i = 0;

	void Start()
	{
		//LSystemItterator itterator = TreeAcer.GetItterator();
		//tree = new Tree(itterator, 9, Vector3.down, 0.7f);

		LSystemItterator itterator = TreePine.GetItterator();
		tree = new Tree(itterator, 15, Vector3.down, 0.2f);
		CreateAsTree();
		
	}

	void CreateAsTree()
	{
		MeshData md = new MeshData();
		md.AddPrimative(tree);
		GetComponent<MeshFilter>().mesh = md.GetMesh();
	}

	void CreateAsSeperate()
	{
		while (true)
		{
			Branch b = GetBranchNumber(tree, i);
			if (b == null) return;
			GameObject g = new GameObject();
			g.AddComponent(typeof(MeshFilter));
			g.AddComponent(typeof(MeshRenderer));
			MeshData md = new MeshData();
			md.AddPrimative(b);
			g.GetComponent<MeshFilter>().mesh = md.GetMesh();
			i++;
		}
	}

	void Update()
	{

	}

	Branch GetBranchNumber(Tree tree, int n)
	{
		Stack<Branch> undrawnBranches = new Stack<Branch>();
		undrawnBranches.Push(tree.GetRoot());
		Gizmos.color = Color.white;
		int k = 0; 
		while (undrawnBranches.Count > 0)
		{
			Branch curr = undrawnBranches.Pop();
			if (k == n) return curr;
			k++;
			
			foreach (Branch b in curr.children)
			{
				undrawnBranches.Push(b);
			}
		}
		return null;
	}
}
