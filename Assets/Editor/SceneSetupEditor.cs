using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneSetup))]
public class SceneSetupEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
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
