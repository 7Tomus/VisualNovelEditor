using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneLinker : ScriptableObject
{
	#region Variables
	private Dictionary<int, List<int>> sceneLinkChain;
	#endregion

	#region PublicMethods
	public void NewSceneLink(int currentSceneNumber)
	{
		List<int> sceneLinks = new List<int>();

		if(sceneLinkChain.TryGetValue(currentSceneNumber, out sceneLinks))
		{
			Debug.Log("Scene already has a child");
		}
		else
		{
			List<int> sceneLink = new List<int>();
			sceneLink.Add(currentSceneNumber + 1);
			sceneLinkChain.Add(currentSceneNumber, sceneLink);
		}
	}
	#endregion
}
