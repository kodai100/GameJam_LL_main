
/*
　リザルトからタイトルへの遷移処理
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
	IEnumerator Start()
	{
		for (var t = 0f; t < 10f; t += Time.deltaTime)
		{
			yield return null;
		}

		FadeManager.Instance.fadeColor = Color.black;
		FadeManager.Instance.LoadLevel("Title", 1.0f);
	}

	public void ToTitle()
	{
		FadeManager.Instance.fadeColor = Color.black;
		FadeManager.Instance.LoadLevel("Title", 1.0f);
	}

	public void ToMain()
	{
		SeManager.Instance.Play("Click");
		FadeManager.Instance.fadeColor = Color.white;
		FadeManager.Instance.LoadLevel("Main", 0.5f);
	}

}