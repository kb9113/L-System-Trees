using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using System.Text;

public class LLangInterpreter 
{
	const string commentPatturn = @"^\s*//.*$";
	const string assignPatturn = @"^\s*(?'Name'\w+)\s*=(?'Expression'.*)$";
	const string symbolPatturn = @"^\s*(?'Symbol'[^\{\}\s])\s*((?'Name'\w+)\s*=(?'Expression'([^,]*(\([^\)]*\))*[^,]*)+)(,\s*(?'Name'\w+)\s*=(?'Expression'([^,]*(\([^\)]*\))*[^,]*)+))*)?$";
	const string controllPatturn = @"^\s*(((?'Name'(if|for|while|loop|else if))\s*\((?'Expression'[^\)]*)\))|(?'Name'else))\s*$";
	const string openPatturn = @"^\s*\{\s*$";
	const string closePatturn = @"^\s*\}\s*$";
	const string defintionPatturn = @"^\s*(?'Symbol'[^(\{|\}|\s)]):\s*$";
	const string axiomPatturn = @"^\s*Axiom:\s*$";
	const string emptyLine = @"^\s*$";
	

	const string includes = "using System;\nusing System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\n";
	StringBuilder codeString = new StringBuilder();
	Dictionary<string, string> stdFuntions = new Dictionary<string, string>();
	
	int lineNumber = 1;
	int tabs = 0;
	Stack<string> varsToAdd = new Stack<string>(); // added on open scope
	Stack<List<string>> variables = new Stack<List<string>>();

	List<string> definitons = new List<string>();

	public string GenerateCode(string className, string raw)
	{
		string[] lines = raw.Split(new char[] { '\n', '\r' });
		bool isInDefintion = false;
		OpenClass(className);

		foreach (string line in lines)
		{
			if (Regex.IsMatch(line, emptyLine)) continue;
			lineNumber++;
			if (Regex.IsMatch(line, commentPatturn)) continue;

			Match match;
			if ((match = Regex.Match(line, assignPatturn)).Success)
			{
				string name = "";
				string expression = "";
				foreach (Group g in match.Groups)
				{
					if (g.Name == "Name") name = g.Value;
					if (g.Name == "Expression") expression = g.Value;
				}
				if (isInDefintion)
				{
					AddLocalVar(name, expression);
				}
				else 
				{
					AddGlobalVar(name, expression);
				}
				continue;
			}
			if ((match = Regex.Match(line, symbolPatturn)).Success)
			{
				List<string> paramaters = new List<string>();
				List<string> expressions = new List<string>();
				string symbol = "";
				foreach (Group g in match.Groups)
				{
					foreach(Capture c in g.Captures)
					{
						if (g.Name == "Name") paramaters.Add(c.Value);
						if (g.Name == "Expression") expressions.Add(c.Value);
						if (g.Name == "Symbol") symbol = c.Value;
					}
				}
				AddSymbol(symbol, paramaters.ToArray(), expressions.ToArray());
				continue;
			}
			if ((match = Regex.Match(line, controllPatturn)).Success)
			{
				string controll = "";
				string expression = "";
				foreach (Group g in match.Groups)
				{
					if (g.Name == "Name") controll = g.Value;
					if (g.Name == "Expression") expression = g.Value;
				}
				AddControll(controll, expression);
				continue;
			}
			if (Regex.IsMatch(line, openPatturn))
			{
				OpenScope();
				continue;
			}
			if (Regex.IsMatch(line, closePatturn))
			{
				CloseScope();
				continue;
			}
			if ((match = Regex.Match(line, defintionPatturn)).Success)
			{
				if (isInDefintion) EndDefinition();
				isInDefintion = true;
				StartDefinition(match.Groups[1].Value);
				continue;
			}
			if ((match = Regex.Match(line, axiomPatturn)).Success)
			{
				if (isInDefintion) EndDefinition();
				isInDefintion = true;
				StartAxiom();
				continue;
			}
			// divide by 2 since we are splititng at \n and \r
			Debug.Log("UnableToPase Line: " + lineNumber);
		}
		if (isInDefintion) EndDefinition();
		CloseClass();
		return codeString.ToString();
	}

	public void AddLine(string line)
	{
		for (int i = 0; i < tabs; i++)
		{
			codeString.Append('\t');
		}
		codeString.Append(line);
	}

	public void OpenScope()
	{
		for (int i = 0; i < tabs; i++)
		{
			codeString.Append('\t');
		}
		variables.Push(new List<string>());
		while (varsToAdd.Count > 0)
		{
			variables.Peek().Add(varsToAdd.Pop());
		}
		codeString.Append("{\n");
		tabs++;
	}

	public void CloseScope()
	{
		tabs--;
		for (int i = 0; i < tabs; i++)
		{
			codeString.Append('\t');
		}
		variables.Pop();
		codeString.Append("}\n");
	}

	public void OpenClass(string name)
	{
		AddLine(includes);
		AddLine("public class " + name + "\n");
		OpenScope();
		AddLine("static System.Random rndGen = new System.Random();\n");
		variables.Peek().Add("rndGen");
		AddLine("public static double rnd => rndGen.NextDouble();\n");
		variables.Peek().Add("rnd");

		// add std funtions 
		stdFuntions.Add("Max", "Math.Max");
		stdFuntions.Add("Min", "Math.Min");

		stdFuntions.Add("Sin", "Math.Sin");
		stdFuntions.Add("Cos", "Math.Cos");
		stdFuntions.Add("Tan", "Math.Tan");
		
		stdFuntions.Add("Pow", "Math.Pow");
	}

	public void CloseClass()
	{
		// impliment interface
		codeString.Append(ItteratorCode());

		// close the class
		CloseScope();
	}

	public string ItteratorCode()
	{
		string result = "\tpublic static LSystemItterator GetItterator()\n";
		result += "\t{\n";
		result += "\t\treturn new LSystemItterator(\n";
		result += "\t\t\tnew Dictionary<char, LSystemItterator.Rule>() {\n";
		foreach (string def in definitons)
		{
			if (def == "Axiom") continue;
			result += "\t\t\t\t{ '"+ def + "', " + def + " }";
			if (def != definitons.Last()) result += ",\n";
			else result += "\n";
		}
		result += "\t\t\t},\n";
		result += "\t\t\tAxiom()\n";
		result += "\t\t);\n";
		result += "\t}\n";
		return result;
	}

	public void AddLocalVar(string name, string expression)
	{
		if (ContainsVar(name))
		{
			AddLine(name + " =" + SymbolEscapeExpression(expression) +";\n");
		}
		else 
		{
			variables.Peek().Add(name);
			AddLine("double " + name + " =" + SymbolEscapeExpression(expression) +";\n");
		}
	}

	public void AddGlobalVar(string name, string expression)
	{
		variables.Peek().Add(name);
		AddLine("public static double " + name + " => " + SymbolEscapeExpression(expression) +";\n");
	}

	public void AddSymbol(string symbol, string[] paramaters, string[] expressions)
	{
		if (symbol == "\\") symbol = "\\\\";
		string line = "symbols.Add(new LSymbol('" + symbol + "'";
		if (paramaters.Length > 0 && paramaters[0] != "") 
		{
			line += ", new Dictionary<string, double>() { ";
			line += GetParamater(paramaters[0], expressions[0]);
		}
		for (int i = 1; i < paramaters.Length; i++)
		{
			line += ", " + GetParamater(paramaters[i], expressions[i]);
		}
		if (paramaters.Length > 0 && paramaters[0] != "") line += " }";
		line += "));\n";
		AddLine(line);
	}

	public string GetParamater(string param, string expression)
	{
		return "{\"" + param + "\"," + SymbolEscapeExpression(expression) + "}";
	}

	public string SymbolEscapeExpression(string expression)
	{
		string result = expression;
		// escape paramaters
		MatchCollection matchs = Regex.Matches(result, @"\b[a-zA-Z]+\b");
		HashSet<string> varNames = new HashSet<string>();
		foreach (Match m in matchs)
		{
			varNames.Add(m.Value);
		}
		foreach(string varName in varNames)
		{
			if (stdFuntions.ContainsKey(varName))
			{
				result = Regex.Replace(result, "\\b" + varName + "\\b", stdFuntions[varName]);
			}
			else if (!ContainsVar(varName) && varName != "")
			{
				result = Regex.Replace(result, "\\b" + varName + "\\b", "sym[\"" + varName + "\"]");
			}
		}
		// add .0 to numbers
		matchs = Regex.Matches(result, @"\b[0-9\.]+\b");
		HashSet<string> numberNames = new HashSet<string>();
		foreach (Match m in matchs)
		{
			numberNames.Add(m.Value);
		}
		foreach (string number in numberNames)
		{
			if (!number.Contains('.'))
			{
				result = Regex.Replace(result, "\\b" + number + "\\b", number + ".0");
			}
		}
		return result;
	}

	public bool ContainsVar(string name)
	{
		foreach(List<string> s in variables)
		{
			if (s.Contains(name)) return true;
		}
		return false;
	}

	public void AddControll(string controll, string expression)
	{
		if (controll == "else")
		{
			AddLine(controll + "\n");
		}
		else if (controll == "loop")
		{
			AddLine("for (int _ = 0; _ < " + SymbolEscapeExpression(expression) + "; _++)\n");
		}
		else if (controll == "for")
		{
			string[] declartionSplit = expression.Split('=');
			string toEscape = String.Join("=", declartionSplit.Skip(1));
			string varName = declartionSplit[0];
			variables.Peek().Add(varName.Trim());
			AddLine(controll + " (double " + varName + "=" + SymbolEscapeExpression(toEscape) + ")\n");
			variables.Peek().Remove(varName.Trim());
			varsToAdd.Push(varName.Trim());
		}
		else 
		{
			AddLine(controll + " (" + SymbolEscapeExpression(expression) + ")\n");
		}
	}

	public void StartAxiom()
	{
		definitons.Add("Axiom");
		AddLine("public static List<LSymbol> Axiom()\n");
		OpenScope();
		AddLine("List<LSymbol> symbols = new List<LSymbol>();\n");
	}

	public void StartDefinition(string name)
	{
		definitons.Add(name);
		AddLine("public static List<LSymbol> " + name + "(LSymbol sym)\n");
		OpenScope();
		AddLine("List<LSymbol> symbols = new List<LSymbol>();\n");
	}

	public void EndDefinition()
	{
		AddLine("return symbols;\n");
		CloseScope();
	}
}
