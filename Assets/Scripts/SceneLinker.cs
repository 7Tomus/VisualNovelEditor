using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneLinker : ScriptableObject
{
	#region Variables
	private Dictionary<int, SceneLink> sceneLinkChain;
	#endregion

	#region PublicMethods
	public int CreateNewScene(int currentSceneNumber)
	{
		int freshSceneNumber;
		SceneCounter sceneCounter = Resources.Load<SceneCounter>("SceneCounter");
		freshSceneNumber = sceneCounter.GetFreshSceneNumber();

		SceneLink currentSceneLinks = new SceneLink();
		SceneLink nextSceneLinks = new SceneLink();

		if(sceneLinkChain.TryGetValue(currentSceneNumber, out currentSceneLinks))
		{
			currentSceneLinks.nextScenes.Add(freshSceneNumber);
			sceneLinkChain.Remove(currentSceneNumber);
			sceneLinkChain.Add(currentSceneNumber, currentSceneLinks);
			nextSceneLinks.previousScenes.Add(currentSceneNumber);
			sceneLinkChain.Add(freshSceneNumber, nextSceneLinks);
		}
		else
		{
			Debug.LogWarning("Current Scene is not inside scene container (◕︿◕✿)");
			currentSceneLinks.nextScenes.Add(freshSceneNumber);
			sceneLinkChain.Add(0, currentSceneLinks);
			nextSceneLinks.previousScenes.Add(0);
			sceneLinkChain.Add(freshSceneNumber, nextSceneLinks);
		}
		return freshSceneNumber;
	}
	#endregion

	#region Structs
	public struct SceneLink
	{
		public List<int> previousScenes;
		public List<int> nextScenes;
	}
	#endregion

}
