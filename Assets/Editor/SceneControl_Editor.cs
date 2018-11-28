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
			if(sceneLinkChain.linkChain.ContainsKey(currentSceneNumber))
			{
				currentSceneLinks = sceneLinkChain.linkChain[currentSceneNumber];
			}
			Debug.Log("Next" + currentSceneLinks.nextScene);
			Debug.Log("Previous" + currentSceneLinks.previousScene);
			refreshSceneData = false;
		}
		GUILayout.BeginVertical();
		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();
		//For initial scene

		if(currentSceneNumber == 0 && currentSceneLinks.nextScene != 0)
		{
			if(GUILayout.Button("Next Scene [" + currentSceneLinks.nextScene + "]"))
			{
				sceneLinkChain.GoToScene(currentSceneNumber, currentSceneLinks.nextScene);
				refreshSceneData = true;
			}
		}
		//For every other scene
		else if(currentSceneNumber != 0)
		{
			if(GUILayout.Button("Previous Scene[" + currentSceneLinks.previousScene + "]"))
			{
				sceneLinkChain.GoToScene(currentSceneNumber, currentSceneLinks.previousScene);
				refreshSceneData = true;
			}
			if(currentSceneLinks.nextScene != 0)
			{
				if(GUILayout.Button("Next Scene[" + currentSceneLinks.nextScene + "]"))
				{
					sceneLinkChain.GoToScene(currentSceneNumber, currentSceneLinks.nextScene);
					refreshSceneData = true;
				}
			}
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
		GUILayout.EndVertical();
		GUILayout.EndVertical();

	}
}
