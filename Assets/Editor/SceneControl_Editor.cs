using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneControl))]
public class SceneControl_Editor : Editor
{
	#region Variables
	private bool refreshSceneData = true;
	private int currentSceneNumber;
	private SceneLinks currentSceneLinks = new SceneLinks();
	SceneLinkChain sceneLinkChain;
	#endregion

	public override void OnInspectorGUI()
	{
		if(refreshSceneData)
		{
			sceneLinkChain = Resources.Load<SceneLinkChain>("SceneLinkChain");
			currentSceneNumber = FindObjectOfType<SceneNumber>().sceneNumber;
			if(currentSceneNumber == 0 && !sceneLinkChain.linkChain.ContainsKey(currentSceneNumber))
			{
				SceneLinks initialSceneLinksForScene0 = new SceneLinks();
				initialSceneLinksForScene0.nextScenes = new List<int>();
				initialSceneLinksForScene0.previousScenes = new List<int>();
				sceneLinkChain.linkChain.Add(0, initialSceneLinksForScene0);
			}
				if(sceneLinkChain.linkChain.ContainsKey(currentSceneNumber))
			{
				currentSceneLinks = sceneLinkChain.linkChain[currentSceneNumber];
			}
			refreshSceneData = false;
		}
		GUILayout.BeginVertical();
		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();

		//For initial scene
		if(currentSceneNumber == 0 && currentSceneLinks.nextScenes.Count != 0)
		{
			GUILayout.BeginVertical();
			for(int i = 0; i< currentSceneLinks.nextScenes.Count; i++)
			{
				if(GUILayout.Button("Next Scene [" + currentSceneLinks.nextScenes[i] + "]"))
				{
					sceneLinkChain.GoToScene(currentSceneNumber, currentSceneLinks.nextScenes[i]);
					refreshSceneData = true;
				}
			}
			GUILayout.EndVertical();
		}
		//For every other scene
		else if(currentSceneNumber != 0)
		{
			GUILayout.BeginVertical();
			for(int i = 0; i < currentSceneLinks.previousScenes.Count; i++)
			{
				if(GUILayout.Button("Previous Scene[" + currentSceneLinks.previousScenes[i] + "]"))
				{
					sceneLinkChain.GoToScene(currentSceneNumber, currentSceneLinks.previousScenes[i]);
					refreshSceneData = true;
				}
			}
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			for(int i = 0; i < currentSceneLinks.nextScenes.Count; i++)
			{
				if(currentSceneLinks.nextScenes.Count != 0)
				{
					if(GUILayout.Button("Next Scene[" + currentSceneLinks.nextScenes[i] + "]"))
					{
						sceneLinkChain.GoToScene(currentSceneNumber, currentSceneLinks.nextScenes[i]);
						refreshSceneData = true;
					}
				}
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginVertical();
		if(GUILayout.Button("New Scene"))
		{
			sceneLinkChain.CreateNewScene(currentSceneNumber);
			EditorUtility.SetDirty(sceneLinkChain);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			refreshSceneData = true;			
		}
		if(GUILayout.Button("ResetAll"))
		{
			sceneLinkChain.ResetLinkChain();
			EditorUtility.SetDirty(sceneLinkChain);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			currentSceneNumber = 0;
			refreshSceneData = true;
		}
		if(GUILayout.Button("GetInfo"))
		{
			Debug.Log("lastSceneNumber" + sceneLinkChain.lastSceneNumber);
			Debug.Log("linkChainLength" + sceneLinkChain.linkChain.Count);
			refreshSceneData = true;
		}

		GUILayout.EndVertical();
		GUILayout.EndVertical();

	}
}
