using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextWriter : MonoBehaviour
{

	#region Variables
	private TextMeshProUGUI mainText;
	private string originalText;
	public float textSpeed = 0.05f;
	#endregion

	private void Awake()
	{
		mainText = GetComponent<TextMeshProUGUI>();
		originalText = mainText.text;
		mainText.text = "";
	}

	void Start()
    {
		StartCoroutine(writeText());
    }

	#region Coroutines
	IEnumerator writeText()
	{
		for(int i = 0; i < originalText.Length; i++)
		{
			yield return new WaitForSeconds(textSpeed);
			mainText.text = mainText.text + originalText[i];
		}
	}
	#endregion
}
