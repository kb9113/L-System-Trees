using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle 
{
	public Vector3 dir;
	public Vector3 right;
	public Vector3 pos;
	public float width;

    public Turtle(Vector3 dir, Vector3 right, Vector3 pos, float width)
    {
        this.dir = dir;
        this.right = right;
        this.pos = pos;
        this.width = width;
    }

	public Turtle(Turtle turtle)
    {
        this.dir = turtle.dir;
        this.right = turtle.right;
        this.pos = turtle.pos;
        this.width = turtle.width;
    }

	public Turtle()
    {
        this.dir = Vector3.up;
        this.right = Vector3.right;
        this.pos = Vector3.zero;
        this.width = 0;
    }

	public void TurnRight(float degrees)
	{
		Vector3 axis = Vector3.Cross(dir, right);
		axis.Normalize();
		Quaternion rotation = Quaternion.AngleAxis(degrees, axis);
		dir = rotation * dir;
		dir.Normalize();
		right = rotation * right;
		right.Normalize();
	}

	public void TurnLeft(float degrees)
	{
		TurnRight(-degrees);
	}

	public void PitchUp(float degrees)
	{
		Quaternion rotation = Quaternion.AngleAxis(degrees, right);
		dir = rotation * dir;
		dir.Normalize();
	}

	public void PitchDown(float degrees)
	{
		PitchUp(-degrees);
	}

	public void RollRight(float degrees)
	{
		Quaternion rotation = Quaternion.AngleAxis(degrees, dir);
		right = rotation * right;
		right.Normalize();
	}

	public void RollLeft(float degrees)
	{
		RollRight(-degrees);
	}

	public void Move(float distance)
	{
		pos += dir * distance;
	}

	public void SetWidth(float width)
	{
		this.width = width;
	}
}