using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneControl))]
public class SceneControl_Editor : Editor
{
	#region Variables
	private bool refreshSceneData = true;
	private SceneData sceneData;
	SceneLinkChain sceneLinkChain;
	#endregion

	public override void OnInspectorGUI()
	{
		if(refreshSceneData)
		{
			sceneLinkChain = Resources.Load<SceneLinkChain>("SceneLinkChain");
			sceneData = FindObjectOfType<SceneData>();
			if(sceneData.sceneNumber == 0 && (sceneData.nextScenes == null || sceneData.previousScenes == null))
			{
				sceneData.nextScenes = new List<int>();
				sceneData.previousScenes = new List<int>();
			}
			refreshSceneData = false;
		}
		GUILayout.BeginVertical();
		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();

		//For initial scene
		if(sceneData.sceneNumber == 0 && sceneData.nextScenes.Count != 0)
		{
			GUILayout.BeginVertical();
			for(int i = 0; i< sceneData.nextScenes.Count; i++)
			{
				if(GUILayout.Button("Next Scene [" + sceneData.nextScenes[i] + "]"))
				{
					sceneLinkChain.GoToScene(sceneData.sceneNumber, sceneData.nextScenes[i]);
					refreshSceneData = true;
				}
			}
			GUILayout.EndVertical();
		}
		//For every other scene
		else if(sceneData.sceneNumber != 0)
		{
			GUILayout.BeginVertical();
			for(int i = 0; i < sceneData.previousScenes.Count; i++)
			{
				if(GUILayout.Button("Previous Scene[" + sceneData.previousScenes[i] + "]"))
				{
					sceneLinkChain.GoToScene(sceneData.sceneNumber, sceneData.previousScenes[i]);
					refreshSceneData = true;
				}
			}
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			for(int i = 0; i < sceneData.nextScenes.Count; i++)
			{
				if(sceneData.nextScenes.Count != 0)
				{
					if(GUILayout.Button("Next Scene[" + sceneData.nextScenes[i] + "]"))
					{
						sceneLinkChain.GoToScene(sceneData.sceneNumber, sceneData.nextScenes[i]);
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
			sceneLinkChain.CreateNewScene(sceneData);
			EditorUtility.SetDirty(sceneLinkChain);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			refreshSceneData = true;			
		}

		GUILayout.EndVertical();
		GUILayout.EndVertical();

	}
}
