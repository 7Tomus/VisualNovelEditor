using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneSetup : MonoBehaviour {

	int number = 0;

	public void NewScene()
	{
		FileUtil.CopyFileOrDirectory("Assets/Scenes/Scene.unity", "Assets/Scenes/Scene" + number + ".unity");
		AssetDatabase.Refresh();
		number++;
	}

}
