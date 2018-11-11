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
	#endregion

	public override void OnInspectorGUI()
	{
		if(refreshSceneData)
		{
			SceneLinkChain sceneLinkChain = Resources.Load<SceneLinkChain>("SceneLinkChain");
			currentSceneNumber = FindObjectOfType<SceneNumber>().sceneNumber;
			if(sceneLinkChain.linkChain.ContainsKey(currentSceneNumber))
			{
				currentSceneLinks = sceneLinkChain.linkChain[currentSceneNumber];
			}
			refreshSceneData = false;
			Debug.Log(currentSceneLinks.nextScene);
			Debug.Log(currentSceneLinks.previousScene);
		}

		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();
		if(currentSceneNumber == 0 && currentSceneLinks.nextScene != 0)
		{

		}
		else
		{
			//TODO ADD BUTTONS
		}
		GUILayout.EndHorizontal();
	}
}
