/*
  クリックによるタイトルからゲームシーンへの遷移
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleClick : MonoBehaviour
{

	// BGM再生用
	void Awake(){
		BgmManager.Instance.Play ("title");
	}

	public void Func()
	{
		SeManager.Instance.Play("Click");
		BgmManager.Instance.TimeToFade = 0.5f;
		BgmManager.Instance.Stop();
		FadeManager.Instance.fadeColor = Color.white;
		FadeManager.Instance.LoadLevel("Main", 0.5f);
	}
}