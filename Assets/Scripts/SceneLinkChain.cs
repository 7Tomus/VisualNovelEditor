using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "SceneLinkChain")]
[Serializable]
public class SceneLinkChain : ScriptableObject {

	#region Variables
	public Dictionary<int, SceneLinks> linkChain = new Dictionary<int, SceneLinks>();
	public int linkChainLength = 0;
	public int lastSceneNumber = 0;
	#endregion

	#region PublicMethods
	public void CreateNewScene(int currentSceneNumber)
	{
		int newSceneNumber = GetNewSceneNumber();
		SceneLinks currentSceneLinks = linkChain[currentSceneNumber];
		currentSceneLinks.nextScenes.Add(newSceneNumber);
		linkChain[currentSceneNumber] = currentSceneLinks;
		SceneLinks newSceneLink = new SceneLinks();
		newSceneLink.nextScenes = new List<int>();
		newSceneLink.previousScenes = new List<int>();
		newSceneLink.previousScenes.Add(currentSceneNumber);
		linkChain.Add(newSceneNumber, newSceneLink);

		AssetDatabase.CopyAsset("Assets/Resources/Scenes/Scene" + currentSceneNumber + ".prefab", "Assets/Resources/Scenes/Scene" + newSceneNumber + ".prefab");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		GameObject newScene = Resources.Load<GameObject>("Scenes/Scene" + newSceneNumber);
		newScene.GetComponent<SceneNumber>().sceneNumber = newSceneNumber;
		GameObject currentScene = GameObject.Find("Scene" + currentSceneNumber);
		DestroyImmediate(currentScene);
		PrefabUtility.InstantiatePrefab(newScene);
		linkChainLength = linkChain.Count;
		EditorUtility.SetDirty(Resources.Load<SceneLinkChain>("SceneLinkChain"));
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

	public void ResetLinkChain()
	{
		linkChain.Clear();
		linkChainLength = linkChain.Count;
		lastSceneNumber = 0;
	}
	#endregion


}

[Serializable]
public struct SceneLinks
{
	public List<int> previousScenes;
	public List<int> nextScenes;
}
