using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class UITextWriter : MonoBehaviour
{

	#region Variables
				public	float			textSpeed = 0.05f;
	[Multiline] public	string[]		paragraphs;
				public	TextMeshProUGUI mainText;
				private string			originalText;
				private int				currentParagraph = 0;
				private IEnumerator		writeCoroutine;
				private int				currentChar = 0;
				private bool			isWriting;
				private SceneData		currentSceneData;
				private SceneLinkChain	sceneLinkChain;
				private UISceneTransition sceneTransition;
	#endregion

	private void Awake()
	{
		sceneLinkChain = Resources.Load<SceneLinkChain>("SceneLinkChain");
		writeCoroutine = WriteText();
		currentSceneData = GetComponent<SceneData>();
		sceneTransition = GetComponent<UISceneTransition>();
	}

	void Start()
    {
		SceneTransitionStart();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			NextText();
		}
	}

	#region Methods

	private async Task SceneTransitionStart()
	{
		await sceneTransition.SceneTransition(TransitionType.fadeIn);
		NextText();
	}

	private async Task SceneTransitionNextScene()
	{
		await sceneTransition.SceneTransition(TransitionType.fadeOut);
		sceneLinkChain.GoToSceneIngame(currentSceneData.sceneNumber, currentSceneData.nextScenes[0]);
	}

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
				if(currentSceneData.nextScenes.Count == 1)
				{
					SceneTransitionNextScene();
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
