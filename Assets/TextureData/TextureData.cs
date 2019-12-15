using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureData 
{
	Color[,] textureColor; // used for the texture colors
	float[,] bumpMap; // used to generate the normal and height map in [-1, 1]
	int width;
	int height;

	// utitlity
	int mod(int x, int m)
	{
		return (x % m + m) % m;
	}

    public TextureData(Color[,] textureColor, float[,] bumpMap)
    {
		if (textureColor.GetLength(0) != bumpMap.GetLength(0) || textureColor.GetLength(1) != bumpMap.GetLength(1))
		{
			throw new System.Exception("texture and bumpMap size mismatch");
		}
        this.textureColor = textureColor;
        this.bumpMap = bumpMap;
        this.width = textureColor.GetLength(0);
        this.height = textureColor.GetLength(1);
    }

	public TextureData(Color[,] textureColor)
    {
        this.textureColor = textureColor;
        this.width = textureColor.GetLength(0);
        this.height = textureColor.GetLength(1);
		this.bumpMap = new float[width, height];
    }

	public Texture2D GetTexture()
	{
		Color[] pixels = new Color[width * height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				pixels[width * y + x] = textureColor[x, y];
			}
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pixels);
		return result;
	}

	public Texture2D GetHeightMap()
	{
		Color[] pixels = new Color[width * height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				float h = bumpMap[x, y];
				float colorValue = (h + 1) * 0.5f;
				pixels[width * y + x] = new Color(colorValue, colorValue, colorValue);
			}
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pixels);
		return result;
	}

	public Texture2D GetNormalMap()
	{
		Color[] pixels = new Color[width * height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				float hc = bumpMap[x, y];
				float hu = bumpMap[x, mod(y + 1, height)];
				float hd = bumpMap[x, mod(y - 1, height)];
				float hr = bumpMap[mod(x + 1, width), y];
				float hl = bumpMap[mod(x - 1, width), y];
				Vector3 vu = new Vector3(0, 1, hu - hc).normalized;
				Vector3 vd = new Vector3(0, -1, hd - hc).normalized;
				Vector3 vr = new Vector3(1, 0, hr - hc).normalized;
				Vector3 vl = new Vector3(-1, 0, hl - hc).normalized;
				Vector3 normal = ((vu + vd + vr + vl) * 0.25f).normalized;
				Vector3 colorVector = 0.5f * (new Vector3(normal.x, normal.y, Mathf.Abs(normal.z)) + Vector3.one);
				pixels[width * y + x] = new Color(colorVector.x, colorVector.y, colorVector.z);
			}
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pixels);
		return result;
	}
}
