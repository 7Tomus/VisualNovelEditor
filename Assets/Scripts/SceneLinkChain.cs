using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneLinkChain : ScriptableObject {

	#region Variables
	public Dictionary<int, SceneLinks> linkChain;
	#endregion

	#region PublicMethods
	public void CreateNewScene(int currentSceneNumber)
	{
		SceneHolder sceneHolder = Resources.Load<SceneHolder>("SceneHolder");
		int newSceneNumber = sceneHolder.GetNewSceneNumber();

		SceneLinks currentSceneLinks = linkChain[currentSceneNumber];
		currentSceneLinks.nextScene = newSceneNumber;
		SceneLinks newSceneLink = new SceneLinks();
		newSceneLink.previousScene = currentSceneNumber;
		linkChain.Add(newSceneNumber, newSceneLink);

		FileUtil.CopyFileOrDirectory("Resources/Scenes/Scene" + currentSceneNumber, "Resources/Scenes/Scene" + newSceneNumber);
		GameObject newScene = Resources.Load<GameObject>("Resources/Scenes/Scene" + newSceneNumber);
		newScene.GetComponent<SceneInfo>().sceneNumber = newSceneNumber;
	}
	#endregion

	#region Structs
	public struct SceneLinks
	{
		public int previousScene;
		public int nextScene;
	}
	#endregion
}
