using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePalm
{
	static System.Random rndGen = new System.Random();
	public static double rnd => rndGen.NextDouble();
	public static double dt =>  4.0;
	public static double tMax =>  350.0;
	public static double pMax =>  0.93;
	public static List<LSymbol> Q(LSymbol sym)
	{
		List<LSymbol> symbols = new List<LSymbol>();
		double propOff = sym["t"] / tMax;
		if (propOff < 1.0)
		{
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", 0.85 + 0.15 * Math.Sin(sym["t"])} }));
			symbols.Add(new LSymbol('^', new Dictionary<string, double>() { {"a", rnd - 0.65} }));
			if (propOff > pMax)
			{
				double dAng = 1.0 / (1.0 - pMax) * (1.0 - propOff) * 110.0 + 15.0;
				symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", 0.1} }));
				for (double i = 0.0; i < rnd * 2.0 + 5.0; i++)
				{
					double rAng = sym["t"] * 10.0 + i * (rnd * 50.0 + 40.0);
					double eDAng = dAng * (rnd * 0.4 + 0.8);
					symbols.Add(new LSymbol('/', new Dictionary<string, double>() { {"a", rAng} }));
					symbols.Add(new LSymbol('&', new Dictionary<string, double>() { {"a", eDAng} }));
					symbols.Add(new LSymbol('['));
					symbols.Add(new LSymbol('A'));
					symbols.Add(new LSymbol(']'));
					symbols.Add(new LSymbol('^', new Dictionary<string, double>() { {"a", eDAng} }));
					symbols.Add(new LSymbol('\\', new Dictionary<string, double>() { {"a", rAng} }));
				}
				symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", 0.05} }));
			}
			else
			{
				symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", 0.15} }));
			}
			symbols.Add(new LSymbol('Q', new Dictionary<string, double>() { {"t", sym["t"] + dt} }));
		}
		else
		{
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", 0.0} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", 0.15} }));
		}
		return symbols;
	}
	public static List<LSymbol> A(LSymbol sym)
	{
		List<LSymbol> symbols = new List<LSymbol>();
		double num = rnd * 5.0 + 30.0;
		for (double i = 0.0; i < num; i++)
		{
			double dAng = (num - 1.0 - i) * (80.0 / num);
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", 0.1 - i * 0.1 / 15.0} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", 0.1} }));
			symbols.Add(new LSymbol('L', new Dictionary<string, double>() { {"rAng", 50.0 * (rnd * 0.4 + 0.8)}, {"dAng", dAng * (rnd * 0.4 + 0.8)} }));
			symbols.Add(new LSymbol('L', new Dictionary<string, double>() { {"rAng", -50.0 * (rnd * 0.4 + 0.8)}, {"dAng", dAng * (rnd * 0.4 + 0.8)} }));
			symbols.Add(new LSymbol('&', new Dictionary<string, double>() { {"a", 1.0} }));
		}
		return symbols;
	}
	public static List<LSymbol> Axiom()
	{
		List<LSymbol> symbols = new List<LSymbol>();
		symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", 0.2} }));
		symbols.Add(new LSymbol('/', new Dictionary<string, double>() { {"a", rnd * 360.0} }));
		symbols.Add(new LSymbol('Q', new Dictionary<string, double>() { {"t", 0.0} }));
		return symbols;
	}
	public static LSystemItterator GetItterator()
	{
		return new LSystemItterator(
			new Dictionary<char, LSystemItterator.Rule>() {
				{ 'Q', Q },
				{ 'A', A },
			},
			Axiom()
		);
	}
}
