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

    void Update()
    {
        //左クリックで遷移
        if (Input.GetMouseButton(0))
		{
			BgmManager.Instance.TimeToFade = 0.5f;
			BgmManager.Instance.Stop ();
            FadeManager.Instance.fadeColor = Color.white;
			FadeManager.Instance.LoadLevel("Main", 0.5f);
        }
    }
}