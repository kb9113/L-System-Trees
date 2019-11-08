using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LLangCompileAll 
{
	public static readonly string[] foldersToCompile = {"Assets/LSystemInterpreter/LSystems"};
	
	[MenuItem("Tools/CompileAllLLang")]
	static void CompileAll()
	{
		List<string> files = new List<string>();
		foreach (string path in foldersToCompile)
		{
			string[] fullPaths = Directory.GetFiles(path, "*.txt");
			for (int i = 0; i < fullPaths.Length; i++) fullPaths[i] = fullPaths[i].Replace('\\', '/');
			files.AddRange(fullPaths);
		}
		foreach (string filePath in files)
		{
			LLangUtility.InterpretScript(filePath);
		}
	}
}
