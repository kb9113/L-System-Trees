using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlendNode 
{

	// takes the arthemtic mean of the 2 inputs
	public float[] Process(float[] a, float[] b)
	{
		if (a.Length != b.Length) throw new ArgumentException("Inputs not of equil size");

		float[] result = new float[a.Length];
		for (int i = 0; i < a.Length; i++)
		{
			result[i] = (a[i] + b[i]) / 2f;
		}
		return result;
	}	
}
