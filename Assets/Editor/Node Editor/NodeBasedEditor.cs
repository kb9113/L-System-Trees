using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeBasedEditor : EditorWindow 
{
	List<EditorNode> nodes;

	GUIStyle nodeStyle;

	[MenuItem("Window/Node Editor")]
	static void OpenWindow()
	{
		NodeBasedEditor window = GetWindow<NodeBasedEditor>();
		window.titleContent = new GUIContent("Node Editor");
	}

	void OnEnable()
	{
		nodeStyle = new GUIStyle();
		nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
	}

	void OnGUI()
	{
		DrawNodes();

		ProcessNodeEvents(Event.current);
		ProcessEvents(Event.current);

		if (GUI.changed) Repaint();
	}

	void DrawNodes()
	{
		if (nodes != null)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				nodes[i].Draw();
			}
		}
	}

	void ProcessEvents(Event e)
	{
		switch (e.type)
		{
			case EventType.MouseDown:
				if (e.button == 1)
				{
					ProcessContextMenu(e.mousePosition);
				}
				break;
		}
	}

	void ProcessNodeEvents(Event e)
	{
		if (nodes != null)
		{
			for (int i = nodes.Count - 1; i >= 0; i--)
			{
				bool guiChanged = nodes[i].ProcessEvents(e);

				if (guiChanged)
				{
					GUI.changed = true;
				}
			}
		}
	}

	void ProcessContextMenu(Vector2 mousePosition)
	{
		GenericMenu genericMenu = new GenericMenu();
		genericMenu.AddItem(new GUIContent("Add Node"), false, () => OnClickAddNode(mousePosition));
		genericMenu.ShowAsContext();
	}

	void OnClickAddNode(Vector2 mousePosition)
	{
		if (nodes == null)
		{
			nodes = new List<EditorNode>();
		}

		nodes.Add(new EditorNode(mousePosition, 200, 50, nodeStyle));
	}
}
