using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormaliseNode  
{
	public float newMin;
	public float newMax;
	
	// normalises all points to be within new min and max
	public float[] Process(float[] input)
	{
		// find the current min and max
		float min = float.MaxValue;
		float max = float.MinValue;
		for (int i = 0; i < input.Length; i++)
		{
			if (input[i] < min) min = input[i];
			if (input[i] > max) max = input[i];
		}

		float[] result = new float[input.Length];
		for (int i = 0; i < input.Length; i++)
		{
			float t = Mathf.InverseLerp(min, max, input[i]);
			result[i] = Mathf.Lerp(newMin, newMax, t);
		}
		return result;
	}
}
