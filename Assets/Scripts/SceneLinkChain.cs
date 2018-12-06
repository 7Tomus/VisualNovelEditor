using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "SceneLinkChain")]
[Serializable]
public class SceneLinkChain : ScriptableObject {

	#region Variables
	public int lastSceneNumber = 0;
	#endregion

	#region PublicMethods
	public void CreateNewScene(SceneData currentSceneData)
	{
		int newSceneNumber = GetNewSceneNumber();
		currentSceneData.nextScenes.Add(newSceneNumber);
		SceneData newSceneData = new SceneData();
		newSceneData.sceneNumber = newSceneNumber;
		newSceneData.nextScenes = new List<int>();
		newSceneData.previousScenes = new List<int>();
		newSceneData.previousScenes.Add(currentSceneData.sceneNumber);

		AssetDatabase.CopyAsset("Assets/Resources/Scenes/Scene" + currentSceneData.sceneNumber + ".prefab", "Assets/Resources/Scenes/Scene" + newSceneNumber + ".prefab");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		GameObject newScene = Resources.Load<GameObject>("Scenes/Scene" + newSceneNumber);
		SceneData sceneDataComponent = newScene.GetComponent<SceneData>();
		sceneDataComponent.sceneNumber = newSceneData.sceneNumber;
		sceneDataComponent.nextScenes = newSceneData.nextScenes;
		sceneDataComponent.previousScenes = newSceneData.previousScenes;
		GameObject currentScene = GameObject.Find("Scene" + currentSceneData.sceneNumber);
		PrefabUtility.SavePrefabAsset(currentScene);
		DestroyImmediate(currentScene);
		PrefabUtility.InstantiatePrefab(newScene);
		//PrefabUtility.SavePrefabAsset(newScene);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	public void GoToScene(int currentSceneNumber, int toSceneNumber)
	{
		DestroyImmediate(GameObject.Find("Scene" + currentSceneNumber));
		PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Scenes/Scene" + toSceneNumber));
	}

	public int GetNewSceneNumber()
	{
		lastSceneNumber++;
		return lastSceneNumber;
	}
	#endregion


}

[Serializable]
public struct SceneLinks
{
	public List<int> previousScenes;
	public List<int> nextScenes;
}
