using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "SceneLinkChain")]
[Serializable]
public class SceneLinkChain : ScriptableObject
{

	#region Variables
	public int lastSceneNumber = 0;
	#endregion

	#region PublicMethods
	public void CreateNewScene(SceneData currentSceneData)
	{
		int newSceneNumber = GetNewSceneNumber();
		currentSceneData.nextScenes.Add(newSceneNumber);
		List<int> nextScenes = new List<int>();
		List<int> previousScenes = new List<int>();
		previousScenes.Add(currentSceneData.sceneNumber);

		AssetDatabase.CopyAsset("Assets/Resources/Scenes/Scene" + currentSceneData.sceneNumber + ".prefab", "Assets/Resources/Scenes/Scene" + newSceneNumber + ".prefab");
		/*
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		*/
		GameObject newScene = Resources.Load<GameObject>("Scenes/Scene" + newSceneNumber);
		SceneData sceneDataComponent = newScene.GetComponent<SceneData>();
		sceneDataComponent.sceneNumber = newSceneNumber;
		sceneDataComponent.nextScenes = nextScenes;
		sceneDataComponent.previousScenes = previousScenes;
		GameObject currentScene = GameObject.Find("Scene" + currentSceneData.sceneNumber);
		PrefabUtility.ApplyPrefabInstance(currentScene, InteractionMode.AutomatedAction);
		DestroyImmediate(currentScene);
		PrefabUtility.InstantiatePrefab(newScene);
		PrefabUtility.SavePrefabAsset(newScene);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	public void GoToScene(int currentSceneNumber, int toSceneNumber, bool save)
	{
		GameObject currentScene = GameObject.Find("Scene" + currentSceneNumber);
		if(save)
		{
			PrefabUtility.ApplyPrefabInstance(currentScene, InteractionMode.AutomatedAction);
		}
		DestroyImmediate(currentScene);
		PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Scenes/Scene" + toSceneNumber));
	}

	public void GoToSceneIngame(int currentSceneNumber, int toSceneNumber)
	{
		Destroy(GameObject.Find("Scene" + currentSceneNumber));
		PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Scenes/Scene" + toSceneNumber));
	}

	public int GetNewSceneNumber()
	{
		lastSceneNumber++;
		return lastSceneNumber;
	}
	#endregion
}