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
	public void CreateNewScene(int currentSceneNumber)
	{
		SceneLink currentSceneLinks = new SceneLink();
		SceneLink nextSceneLinks = new SceneLink();
		SceneCounter sceneCounter = Resources.Load<SceneCounter>("SceneCounter");

		if(sceneLinkChain.TryGetValue(currentSceneNumber, out currentSceneLinks))
		{
			//FILL CURRENT SCENE LINK AND NEXT SCENE LINK
			int freshSceneNumber = sceneCounter.GetFreshSceneNumber();

			SceneLink sceneLink = new SceneLink();

			sceneLink.nextScenes.Add(sceneCounter.GetFreshSceneNumber());
			sceneLink.previousScenes.Add(currentSceneNumber - 1);
			sceneLinkChain.Add(currentSceneNumber, sceneLink);
		}
		else
		{
			Debug.LogWarning("Current Scene is not inside scene container (◕︿◕✿)");
		}
	}
	#endregion
}
