using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSymbol
{
	public char letter;
	public Dictionary<string, double> paramaters;

    public double this[string key]
    {
        get 
        {
            return paramaters[key];
        }
    }

    public LSymbol(char letter, Dictionary<string, double> paramaters)
    {
        this.letter = letter;
        this.paramaters = paramaters;
    }

    public LSymbol(char letter, string paramaterName, double paramaterValue)
    {
        this.letter = letter;
        this.paramaters = new Dictionary<string, double>();
        paramaters.Add(paramaterName, paramaterValue);
    }

    public LSymbol(char letter)
    {
        this.letter = letter;
        this.paramaters = new Dictionary<string, double>();
    }
}
