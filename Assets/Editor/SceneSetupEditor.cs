using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SceneSetup))]
public class SceneSetupEditor : Editor {

	public override void OnInspectorGUI()
	{

		List<int> linklist = new List<int>();
		linklist.Add(1);
		linklist.Add(2);
		linklist.Add(3);

		List<int> nextlinklist = new List<int>();
		nextlinklist.Add(1);

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Previous Scene"))
		{

		}
		GUILayout.BeginVertical();
		foreach(int i in linklist)
		{
			if(GUILayout.Button("Next Scene" + i))
			{

			}
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		foreach(int i in nextlinklist)
		{
			if(GUILayout.Button("New Scene" + i))
			{
				nextlinklist.Add(i + 1); //TODO
			}
		}
		GUILayout.Space(20);
		SceneSetup sceneSetup = (SceneSetup)target;
		if(GUILayout.Button("New Scene"))
		{
			SceneCounter sceneCounter = Resources.Load<SceneCounter>("SceneCounter");
			sceneSetup.NewScene(sceneCounter.GetSceneNumber());
			EditorUtility.SetDirty(sceneCounter);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}
