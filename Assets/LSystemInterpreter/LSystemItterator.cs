using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LSystemItterator 
{
	public delegate List<LSymbol> Rule(LSymbol symbol);
	Dictionary<char, Rule> rules;
	List<LSymbol> currentString;	
	int completeItterations;

    public LSystemItterator(Dictionary<char, Rule> rules, List<LSymbol> currentString)
    {
        this.rules = rules;
        this.currentString = currentString;
		completeItterations = 0;
    }

	public void Itterate()
	{
		completeItterations += 1;
		List<LSymbol> outputString = new List<LSymbol>();
		foreach (LSymbol sym in currentString)
		{
			if (rules.ContainsKey(sym.letter))
			{
				Rule rule = rules[sym.letter];
				Debug.Log(sym.letter);
				outputString.AddRange(rule(sym));
			}
			else 
			{
				outputString.Add(sym);
			}
		}
		currentString = outputString;
	}

	public void Itterate(int n)
	{
		for (int i = 0; i < n; i++)
		{
			Itterate();
		}
	}

	public List<LSymbol> GetString()
	{
		return currentString;
	}
}
