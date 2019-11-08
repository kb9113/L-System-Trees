using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LLangUtility 
{
	public static void InterpretScript(string path)
	{
		StreamReader reader = new StreamReader(path);
		string source = reader.ReadToEnd();

		string[] tokens = path.Split('/');
		string name = tokens.Last().Replace(".txt", "");

		LLangInterpreter interpreter = new LLangInterpreter();
		string code = interpreter.GenerateCode(name, source);
		
		string newPath = path.Replace(".txt", ".cs");
		File.WriteAllText(newPath, code);
		AssetDatabase.ImportAsset(newPath);
	}
}
