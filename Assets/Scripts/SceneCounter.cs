using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneCounter : ScriptableObject
{
	#region Variables
	public int sceneNumber;
	#endregion

	#region PublicMethods
	public int GetSceneNumber()
	{
		sceneNumber++;
		return sceneNumber;
	}
	#endregion
}