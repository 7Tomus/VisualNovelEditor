using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextWriter : MonoBehaviour
{

	#region Variables
	public float textSpeed = 0.05f;
	[Multiline] public string[] paragraphs;
	private TextMeshProUGUI mainText;
	private string originalText;
	private int currentParagraph = 0;
	private IEnumerator writeCoroutine;
	int currentChar = 0; 
	#endregion

	private void Awake()
	{
		mainText = GetComponent<TextMeshProUGUI>();
		writeCoroutine = WriteText();
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
		if(Input.GetKeyDown(KeyCode.K))
		{
			StopCoroutine(writeCoroutine);
		}
	}

	#region Methods
	private void NextText()
	{
		StopCoroutine(writeCoroutine);
		writeCoroutine = null;
		writeCoroutine = WriteText();
		currentChar = 0;
		if(paragraphs.Length > currentParagraph)
		{
			originalText = paragraphs[currentParagraph];
			mainText.text = "";
			currentParagraph++;
			StartCoroutine(writeCoroutine);
		}
	}
	#endregion

	#region Coroutines
	IEnumerator WriteText()
	{
		Debug.Log("write coroutine started");
		for(currentChar = 0; currentChar < originalText.Length; currentChar++)
		{
			yield return new WaitForSeconds(textSpeed);
			mainText.text = mainText.text + originalText[currentChar];
		}
	}
	#endregion
}
