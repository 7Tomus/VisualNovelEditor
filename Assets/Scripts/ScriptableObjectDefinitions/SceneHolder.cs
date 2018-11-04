using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneHolder: ScriptableObject
{
	#region Variables
	public List<GameObject> sceneList;
	#endregion

	#region PublicMethods
	public int GetNewSceneNumber()
	{
		return SearchForHighestSceneNumber() + 1;
	}
	#endregion

	#region PrivateMethods
	private int SearchForHighestSceneNumber()
	{
		int highestSceneNumber = 0;
		foreach(GameObject scene in sceneList)
		{
			SceneInfo sceneInfo = scene.GetComponent<SceneInfo>();
			if(sceneInfo != null && sceneInfo.sceneNumber > highestSceneNumber)
			{
				highestSceneNumber = sceneInfo.sceneNumber;
			}
		}
		return highestSceneNumber;
	}
	#endregion
}