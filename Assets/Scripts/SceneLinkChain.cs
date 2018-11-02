using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLinkChain : ScriptableObject {

	#region Variables
	public Dictionary<int, SceneLinks> linkChain;
	public struct SceneLinks
	{
		public int previousScene;
		public int nextScene;
	}
	#endregion

	#region PublicMethods
	public void CreateNewScene(int currentSceneNumber)
	{

	}
	#endregion
}
