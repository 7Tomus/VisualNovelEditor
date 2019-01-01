using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class UISceneTransition : MonoBehaviour
{
	#region Variables
	public Image blackImage;
	private Color transparent = new Color(0, 0, 0, 0);
	#endregion

	private void Awake()
	{
		blackImage.gameObject.SetActive(true);
	}

	#region Methods

	public async Task SceneTransition(TransitionType transitionType)
	{
		switch(transitionType)
		{
			case TransitionType.fadeIn :
				{
					await FadeIn();
					break;
				}
			case TransitionType.fadeOut :
				{
					await FadeOut();
					break;
				}
		}
	}
	#endregion

	#region Async
	async Task FadeOut()
	{

		float progress = 0;
		while(progress < 1)
		{
			await new WaitForFixedUpdate();
			blackImage.color = Color.Lerp(transparent, Color.black, progress);
			progress = progress + Time.deltaTime;
		}
	}

	async Task FadeIn()
	{
		float progress = 0;
		while(progress < 1)
		{
			await new WaitForFixedUpdate();
			blackImage.color = Color.Lerp(Color.black, transparent, progress);
			progress = progress + Time.deltaTime;
		}
	}
	#endregion
}

public enum TransitionType
{
	fadeIn	= 0,
	fadeOut = 1
}
