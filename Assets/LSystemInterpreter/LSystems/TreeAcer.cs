using System;
using System.Collections;
using System.Collections.Generic;

public class TreeAcer
{
	static System.Random rndGen = new System.Random();
	public static double rnd => rndGen.NextDouble();
	public static double nLeaves =>  15.0;
	public static double widthD =>  0.4 / 8.0;
	public static double lengthD =>  0.1;
	public static double itterations =>  9.0;
	public static double DAngle1 =>  rnd * 40.0 + 60.0;
	public static double DAngle2 =>  rnd * 40.0 + 100.0;
	public static double BranchAngle =>  rnd * 15.0 + 10.0;
	public static List<LSymbol> F(LSymbol sym)
	{
		List<LSymbol> symbols = new List<LSymbol>();
		symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"]}, {"leaves", 0.0} }));
		return symbols;
	}
	public static List<LSymbol> A(LSymbol sym)
	{
		List<LSymbol> symbols = new List<LSymbol>();
		double rand = rnd / (0.24 * Math.Pow(sym["l"], 2.5));
		if (sym["l"] == 2.0)
		{
			rand = rand + 0.5;
		}
		if (rand < 0.4)
		{
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0} }));
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
			symbols.Add(new LSymbol('&', new Dictionary<string, double>() { {"a", BranchAngle} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0}, {"leaves", nLeaves}, {"leafDAngle", 40.0}, {"leafRAngle", 140.0} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"w", sym["w"] - widthD}, {"l", sym["l"] - lengthD} }));
			symbols.Add(new LSymbol(']'));
		}
		else if (rand < 0.8)
		{
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0} }));
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
			symbols.Add(new LSymbol('&', new Dictionary<string, double>() { {"a", BranchAngle} }));
			symbols.Add(new LSymbol('/', new Dictionary<string, double>() { {"a", 90.0} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0}, {"leaves", nLeaves}, {"leafDAngle", 40.0}, {"leafRAngle", 140.0} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"w", sym["w"] - widthD}, {"l", sym["l"] - lengthD} }));
			symbols.Add(new LSymbol(']'));
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
			symbols.Add(new LSymbol('^', new Dictionary<string, double>() { {"a", BranchAngle} }));
			symbols.Add(new LSymbol('\\', new Dictionary<string, double>() { {"a", 90.0} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0}, {"leaves", nLeaves}, {"leafDAngle", 40.0}, {"leafRAngle", 140.0} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"w", sym["w"] - widthD}, {"l", sym["l"] - lengthD} }));
			symbols.Add(new LSymbol(']'));
		}
		else
		{
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0} }));
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
			symbols.Add(new LSymbol('&', new Dictionary<string, double>() { {"a", BranchAngle} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0}, {"leaves", nLeaves}, {"leafDAngle", 40.0}, {"leafRAngle", 140.0} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"w", sym["w"] - widthD}, {"l", sym["l"] - lengthD} }));
			symbols.Add(new LSymbol(']'));
			symbols.Add(new LSymbol('/', new Dictionary<string, double>() { {"a", DAngle1} }));
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
			symbols.Add(new LSymbol('&', new Dictionary<string, double>() { {"a", BranchAngle} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0}, {"leaves", nLeaves}, {"leafDAngle", 40.0}, {"leafRAngle", 140.0} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"w", sym["w"] - widthD}, {"l", sym["l"] - lengthD} }));
			symbols.Add(new LSymbol(']'));
			symbols.Add(new LSymbol('/', new Dictionary<string, double>() { {"a", DAngle2} }));
			symbols.Add(new LSymbol('['));
			symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", sym["w"]} }));
			symbols.Add(new LSymbol('&', new Dictionary<string, double>() { {"a", BranchAngle} }));
			symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", sym["l"] / 2.0}, {"leaves", nLeaves}, {"leafDAngle", 40.0}, {"leafRAngle", 140.0} }));
			symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"w", sym["w"] - widthD}, {"l", sym["l"] - lengthD} }));
			symbols.Add(new LSymbol(']'));
		}
		return symbols;
	}
	public static List<LSymbol> Axiom()
	{
		List<LSymbol> symbols = new List<LSymbol>();
		symbols.Add(new LSymbol('!', new Dictionary<string, double>() { {"w", 0.7} }));
		symbols.Add(new LSymbol('F', new Dictionary<string, double>() { {"l", 0.5} }));
		symbols.Add(new LSymbol('/', new Dictionary<string, double>() { {"a", 45.0} }));
		symbols.Add(new LSymbol('A', new Dictionary<string, double>() { {"w", 0.4}, {"l", 2.0} }));
		return symbols;
	}
}
