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
		sceneLinkChain = Resources.Load<SceneLinkChain>("SceneLinkChain");
		if(refreshSceneData)
		{
			
			currentSceneNumber = FindObjectOfType<SceneNumber>().sceneNumber;
			if(sceneLinkChain.linkChain.ContainsKey(currentSceneNumber))
			{
				currentSceneLinks = sceneLinkChain.linkChain[currentSceneNumber];
			}
			refreshSceneData = false;
			Debug.Log("nextScene: " + currentSceneLinks.nextScene);
			Debug.Log("previousScene: " + currentSceneLinks.previousScene);
		}

		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();
		if(currentSceneNumber == 0 && currentSceneLinks.nextScene != 0)
		{
			if(GUILayout.Button("Next Scene [" + currentSceneLinks.nextScene + "]"))
			{
			}
		}
		else if(currentSceneNumber != 0)
		{
			if(GUILayout.Button("Previous Scene[" + currentSceneLinks.previousScene + "]"))
			{
			}
			if(currentSceneLinks.nextScene != 0)
			{
				if(GUILayout.Button("Next Scene[" + currentSceneLinks.nextScene + "]"))
				{
				}
			}
		}
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
			refreshSceneData = true;
		}

		GUILayout.EndHorizontal();
	}
}
