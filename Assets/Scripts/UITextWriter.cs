using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextWriter : MonoBehaviour
{

	#region Variables
				public	float			textSpeed = 0.05f;
	[Multiline] public	string[]		paragraphs;
				private TextMeshProUGUI mainText;
				private string			originalText;
				private int				currentParagraph = 0;
				private IEnumerator		writeCoroutine;
				private int				currentChar = 0;
				private bool			isWriting;
				private SceneData		currentSceneData;
	#endregion

	private void Awake()
	{
		mainText = GetComponent<TextMeshProUGUI>();
		writeCoroutine = WriteText();
		currentSceneData = gameObject.GetComponentInParent<SceneData>();
	}

	void Start()
    {
		NextText();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			NextText();
		}
	}

	#region Methods
	private void NextText()
	{
		StopCoroutine(writeCoroutine);
		if(!isWriting)
		{
			if(paragraphs.Length > currentParagraph)
			{
				currentChar = 0;
				originalText = paragraphs[currentParagraph];
				mainText.text = "";
				currentParagraph++;
				writeCoroutine = null;
				writeCoroutine = WriteText();
				StartCoroutine(writeCoroutine);
			}
			else
			{
				SceneLinkChain sceneLinkChain = Resources.Load<SceneLinkChain>("SceneLinkChain");
				if(currentSceneData.nextScenes.Count == 1)
				{
					sceneLinkChain.GoToSceneIngame(currentSceneData.sceneNumber, currentSceneData.nextScenes[0]);
				}
				else
				{
					//TODO multiple choice
				}
			}
		}
		else
		{
			isWriting = false;
			mainText.text = originalText;
		}
	}

	private void GoToNextScene()
	{

	}
	#endregion

	#region Coroutines
	IEnumerator WriteText()
	{
		isWriting = true;
		for(currentChar = 0; currentChar < originalText.Length; currentChar++)
		{
			yield return new WaitForSeconds(textSpeed);
			mainText.text = mainText.text + originalText[currentChar];
		}
		isWriting = false;
	}
	#endregion
}
