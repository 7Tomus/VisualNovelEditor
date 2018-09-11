using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneSetup : MonoBehaviour {

	public void NewScene(int number)
	{
		FileUtil.CopyFileOrDirectory("Assets/Scenes/Scene.unity", "Assets/Scenes/Scene" + number + ".unity");
	}
}
