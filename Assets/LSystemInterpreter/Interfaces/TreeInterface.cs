using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeInterface : ILLangInterface
{
	public string GetImplimentation(string[] definitons, string[] vars)
	{
		if (!definitons.Contains("Axiom")) 
		{
			Debug.Log("Missing Axiom");
			return "";
		}
		if (!vars.Contains("itterations") || !vars.Contains("tropismX") || !vars.Contains("tropismY") || !vars.Contains("tropismZ") || !vars.Contains("initialWidth"))
		{
			Debug.Log("Missing required variable");
			return "";
		}

		string result = "\tpublic static Tree GenerateTree()\n";
		result += "\t{\n";
		result += "\t\tLSystemItterator sys = new LSystemItterator(\n";
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
		result += "\t\tsys.Itterate((int)itterations);\n";
		result += "\t\treturn Tree.ParseTree(sys.GetString().ToArray(), new Vector3((float)tropismX, (float)tropismY, (float)tropismZ), (float)initialWidth);\n";
		result += "\t}\n";
		return result;
	}
}
