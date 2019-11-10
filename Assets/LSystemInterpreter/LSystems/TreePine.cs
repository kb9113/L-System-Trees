using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePine
{
	static System.Random rndGen = new System.Random();
	public static double rnd => rndGen.NextDouble();
	public static double lengthR =>  0.8;
	public static double widthR =>  0.8;
	public static double branchLengthR =>  0.93;
	public static double secondBranceAngle =>  80.0;
	public static double secondBranceAngleV =>  30.0;
	public static double nLeaves =>  300.0;
	public static double itterations =>  15.0;
	public static List<LSymbol> Q(LSymbol sym)
	{
		List<LSymbol> symbols = new List<LSymbol>();
		symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
		symbols.Add(new LSymbol('&', new Dictionary<string, double>() { {"a", 90.0} }));
		symbols.Add(new LSymbol('+', new Dictionary<string, double>() { {"a", rnd * 360.0} }));
		symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["bw"]} }));
		double bCount = (rnd * 2.0) + 5.0;
		for (int _ = 0; _ < bCount; _++)
		{
			double rand = rnd * 130.0 / bCount;
			symbols.Add(new LSymbol('+', new Dictionary<string, double>() { {"a", rand} }));
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('^', new Dictionary<string, double>() { {"a", 5.0 / Math.Max(sym["bl"] * sym["bl"], 0.05) - 30.0 * (rnd * 0.2 + 0.9)} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"l", sym["bl"]}, {"w", sym["bw"]} }));
			symbols.Add(new LSymbol(']'));
			symbols.Add(new LSymbol('+', new Dictionary<string, double>() { {"a", 360.0 / bCount - rand} }));
		}
		symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
		symbols.Add(new LSymbol('$'));
		symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"]}, {"leaves", sym["l"] * nLeaves / 3.0}, {"leafDAngle", 20.0}, {"leafRAngle", 140.0} }));
		symbols.Add(new LSymbol('Q', new Dictionary<string, double>() { {"w", sym["w"] - 0.2 / 15.0}, {"l", sym["l"] * 0.95}, {"bw", sym["bw"] * widthR}, {"bl", sym["bl"] * branchLengthR} }));
		symbols.Add(new LSymbol('%'));
		return symbols;
	}
	public static List<LSymbol> A(LSymbol sym)
	{
		List<LSymbol> symbols = new List<LSymbol>();
		double ang = rnd * secondBranceAngleV + secondBranceAngle;
		if (rnd < sym["l"])
		{
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
			symbols.Add(new LSymbol('^', new Dictionary<string, double>() { {"a", rnd * 15.0 - 5.0} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"]}, {"leaves", sym["l"] * nLeaves / 3.0}, {"leafDAngle", 40.0}, {"leafRAngle", 140.0} }));
			symbols.Add(new LSymbol('-', new Dictionary<string, double>() { {"a", ang / 2.0} }));
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"l", sym["l"] * lengthR}, {"w", sym["w"] * widthR} }));
			symbols.Add(new LSymbol(']'));
			symbols.Add(new LSymbol('+', new Dictionary<string, double>() { {"a", ang} }));
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"l", sym["l"] * lengthR}, {"w", sym["w"] * widthR} }));
			symbols.Add(new LSymbol(']'));
			symbols.Add(new LSymbol('-', new Dictionary<string, double>() { {"a", ang / 2.0} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"l", sym["l"] * lengthR}, {"w", sym["w"] * widthR} }));
		}
		else
		{
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
			symbols.Add(new LSymbol('^', new Dictionary<string, double>() { {"a", rnd * 15.0 - 5.0} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"]}, {"leaves", sym["l"] * nLeaves / 3.0}, {"leafDAngle", 40.0}, {"leafRAngle", 140.0} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"l", sym["l"] * lengthR}, {"w", sym["w"] * widthR} }));
		}
		return symbols;
	}
	public static List<LSymbol> Axiom()
	{
		List<LSymbol> symbols = new List<LSymbol>();
		symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", 0.2} }));
		symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"1", 0.6} }));
		symbols.Add(new LSymbol('Q', new Dictionary<string, double>() { {"w", 0.2}, {"bw", 0.05}, {"l", 0.5}, {"bl", 0.4} }));
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
