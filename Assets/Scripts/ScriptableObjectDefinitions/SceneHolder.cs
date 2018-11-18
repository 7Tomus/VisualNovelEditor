using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneHolder")]
public class SceneHolder: ScriptableObject
{
	#region Variables
	public int lastSceneNumber = 0;
	public List<GameObject> sceneList;
	#endregion

	#region PublicMethods
	public int GetNewSceneNumber()
	{
		lastSceneNumber++;
		return lastSceneNumber;
		//return SearchForHighestSceneNumber() + 1;
	}
	#endregion

	#region PrivateMethods
	private int SearchForHighestSceneNumber()
	{
		int highestSceneNumber = 0;
		foreach(GameObject scene in sceneList)
		{
			SceneNumber sceneInfo = scene.GetComponent<SceneNumber>();
			if(sceneInfo != null && sceneInfo.sceneNumber > highestSceneNumber)
			{
				highestSceneNumber = sceneInfo.sceneNumber;
			}
		}
		return highestSceneNumber;
	}
	#endregion
}