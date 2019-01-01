using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MultiChoice : MonoBehaviour
{
	#region Variables
	public GameObject choicePrefab;
	private List<GameObject> choiceGameObjects;
	private List<Button> choiceButtons;
	private List<TextMeshProUGUI> choiceTexts;
	private SceneLinkChain sceneLinkChain;
	private SceneData sceneData;
	#endregion

	#region Methods
	private void CreateChoices(List<string> choiceStrings, int currentSceneNumber, List<UnityAction> choiceButtonActions = null)
	{
		sceneData = GetComponentInParent<SceneData>();

		foreach(string s in choiceStrings)
		{
			GameObject choice = Instantiate(choicePrefab, gameObject.transform);
			choiceGameObjects.Add(choice);
			choiceButtons.Add(choice.GetComponent<Button>());
			choiceTexts.Add(choice.GetComponentInChildren<TextMeshProUGUI>());
		}

		if(choiceButtonActions == null)
		{
			choiceButtonActions = new List<UnityAction>();
			sceneLinkChain = Resources.Load<SceneLinkChain>("SceneLinkChain");

			for(int i = 0; i<sceneData.nextScenes.Count; i++)
			{
				//choiceButtons[i].onClick.AddListener(() => sceneLinkChain.GoToSceneIngame(sceneData.sceneNumber, sceneData.nextScenes[i]));
				choiceButtons[i].onClick.AddListener(delegate {sceneLinkChain.GoToSceneIngame(sceneData.sceneNumber, sceneData.nextScenes[i]);});
			}
		}
	}
	#endregion
}
