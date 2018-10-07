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
	public void NewSceneLink(int currentSceneNumber)
	{
		SceneLink sceneLinks = new SceneLink();

		if(sceneLinkChain.TryGetValue(currentSceneNumber, out sceneLinks))
		{
			Debug.Log("Scene already has a child");
		}
		else
		{
			SceneLink sceneLink = new SceneLink();
			sceneLink.nextScenes.Add(currentSceneNumber + 1);
			sceneLink.previousScene = currentSceneNumber - 1;
			sceneLinkChain.Add(currentSceneNumber, sceneLink);
		}
	}
	#endregion
}
