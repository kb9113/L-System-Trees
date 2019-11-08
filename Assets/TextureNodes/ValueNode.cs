using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueNode  
{
	public float value;

	public int width;
	public int height;


	// returns a map with all values equil to value
	public float[] Process()
	{
		float[] result = new float[width * height];
		for (int i = 0; i < width * height; i++)
		{
			result[i] = value;
		}
		return result;
	}
}
