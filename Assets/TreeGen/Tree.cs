﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tree 
{
    Branch root;

    public Tree(Branch root)
    {
        this.root = root;
    }

    public Branch GetRoot()
    {
        return root;
    }

    public MeshData GetMeshData()
    {
        List<Branch> undrawnBranches = new List<Branch>() { root };
		MeshData md = new MeshData();
		while (undrawnBranches.Count > 0)
		{
			md.AddMesh(undrawnBranches[0].GetMeshData());
			foreach (Branch b in undrawnBranches[0].children)
			{
				undrawnBranches.Add(b);
			}
			undrawnBranches.RemoveAt(0);
		}
        return md;
    }

    public static Tree ParseTree(LSymbol[] instructionStrings, Vector3 tropism, float inniatalWidth)
	{
		Turtle turtle = new Turtle();
		Branch branch = new Branch(turtle.pos, inniatalWidth);
        Branch root = branch;
		Stack<Branch> branchStack = new Stack<Branch>();
		Stack<Turtle> turtleStack = new Stack<Turtle>();

		foreach (LSymbol sym in instructionStrings)
		{
			char letter = sym.letter;

			// start branch
			if (letter == '[')
			{
				turtleStack.Push(new Turtle(turtle));
				branchStack.Push(branch);
				branch = new Branch(turtle.pos, turtle.width);
			}
			// end branch
			if (letter == ']')
			{
				branchStack.Peek().AddChild(branch);
				branch = branchStack.Pop();
				turtle = turtleStack.Pop();
			}

			// turtle movement
			if (letter == '+')
			{
				turtle.TurnLeft((float)sym.paramaters["a"]);
			}
			if (letter == '-')
			{
				turtle.TurnRight((float)sym.paramaters["a"]);
			}
			if (letter == '^')
			{
				turtle.PitchUp((float)sym.paramaters["a"]);
			}
			if (letter == '&')
			{
				turtle.PitchDown((float)sym.paramaters["a"]);
			}
			if (letter == '/')
			{
				turtle.RollRight((float)sym.paramaters["a"]);
			}
			if (letter == '\\')
			{
				turtle.RollLeft((float)sym.paramaters["a"]);
			}
			// set turtle width
			if (letter == '!')
			{
				turtle.SetWidth((float)sym.paramaters["w"]);
			}
			// reset turtle to vertical
			if (letter == '$')
			{
				turtle.dir = Vector3.up;
				turtle.right = Vector3.right;
			}
			
			// move turlte foward
			if (letter == 'F')
			{
				// applies tropism force
				Vector3 cross = Vector3.Cross(turtle.dir, tropism);
				float alpha = cross.magnitude;
				cross.Normalize();
				Quaternion rotatin = Quaternion.AngleAxis(alpha, cross);
				turtle.dir = rotatin * turtle.dir;
				turtle.right = rotatin * turtle.right;
				// moves turle
				turtle.Move((float)sym.paramaters["l"]);
				branch.AddAfter(turtle.pos, turtle.width);
			}
			// taper end of branch
			if (letter == 'A' || letter == '%')
			{
				branch.Taper();
			}
			// add leaf
			if (letter == 'L')
			{
				// TODO: add leaf
			}
		}
		return new Tree(root);
	}
}
