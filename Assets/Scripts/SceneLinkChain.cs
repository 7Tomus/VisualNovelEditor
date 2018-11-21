using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "SceneLinkChain")]
public class SceneLinkChain : ScriptableObject {

	#region Variables
	public Dictionary<int, SceneLinks> linkChain = new Dictionary<int, SceneLinks>();
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

		//GameObject currentScene = GameObject.Find("Scene" + currentSceneNumber);
		//var currentScene = Resources.Load<GameObject>("Scenes/Scene" + currentSceneNumber);
		//var clonedAsset = Instantiate(currentScene);
		try
		{
			//PrefabUtility.
			Debug.Log("Scene" + currentSceneNumber + ".prefab");
			AssetDatabase.CopyAsset("Assets/Resources/Scenes/Scene" + currentSceneNumber + ".prefab", "Assets/Resources/Scenes/Scene" + newSceneNumber + ".prefab");
			AssetDatabase.Refresh();
			//AssetDatabase.CreateAsset(clonedAsset, "Assets/Resources/Scenes/Scene" + newSceneNumber);
		}
		catch(Exception e)
		{
			EditorUtility.DisplayDialog("Error", "Error cloning asset", "Ok");
			return;
		}
		//AssetDatabase.CopyAsset("Resources/Scenes/Scene" + currentSceneNumber, "Resources/Scenes/Scene" + newSceneNumber);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		/*
		//FileUtil.CopyFileOrDirectory("Resources/Scenes/Scene" + currentSceneNumber + ".prefab", "Resources/Scenes/Scene" + newSceneNumber + ".prefab");
		GameObject newScene = Resources.Load<GameObject>("Scenes/Scene" + newSceneNumber);
		newScene.GetComponent<SceneNumber>().sceneNumber = newSceneNumber;
		GameObject currentScene = GameObject.Find("Scene" + currentSceneNumber);
		Destroy(currentScene);
		Instantiate(newScene);
		EditorUtility.SetDirty(sceneHolder);
		*/
	}
	#endregion


}
