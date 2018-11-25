using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "SceneLinkChain")]
public class SceneLinkChain : ScriptableObject {

	#region Variables
	public Dictionary<int, SceneLinks> linkChain = new Dictionary<int, SceneLinks>();
	public int linkChainLength = 0;
	#endregion

	#region PublicMethods
	public void CreateNewScene(int currentSceneNumber)
	{
		SceneHolder sceneHolder = Resources.Load<SceneHolder>("SceneHolder");
		int newSceneNumber = sceneHolder.GetNewSceneNumber();
		if(linkChain.Count == 0)
		{
			linkChain.Add(0, new SceneLinks());
		}
		SceneLinks currentSceneLinks = linkChain[currentSceneNumber];
		currentSceneLinks.nextScene = newSceneNumber;
		SceneLinks newSceneLink = new SceneLinks();
		newSceneLink.previousScene = currentSceneNumber;
		linkChain.Add(newSceneNumber, newSceneLink);

		Debug.Log("Scene" + currentSceneNumber + ".prefab");
		AssetDatabase.CopyAsset("Assets/Resources/Scenes/Scene" + currentSceneNumber + ".prefab", "Assets/Resources/Scenes/Scene" + newSceneNumber + ".prefab");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		GameObject newScene = Resources.Load<GameObject>("Scenes/Scene" + newSceneNumber);
		newScene.GetComponent<SceneNumber>().sceneNumber = newSceneNumber;
		GameObject currentScene = GameObject.Find("Scene" + currentSceneNumber);
		if(currentScene == null)
		{
			currentScene = GameObject.Find("Scene" + currentSceneNumber + "(Clone)");
		};
		DestroyImmediate(currentScene);
		Instantiate(newScene);
		EditorUtility.SetDirty(sceneHolder);
		linkChainLength = linkChain.Count;
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	public void ResetLinkChain()
	{
		linkChain.Clear();
		linkChainLength = linkChain.Count;
	}
	#endregion


}
