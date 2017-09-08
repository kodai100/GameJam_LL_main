
/*
　リザルトからタイトルへの遷移処理
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ResultController : MonoBehaviour{

	#region private variables
	private EventSystem mEventSystem;
	#endregion

	#region private functions
	void Start(){
		mEventSystem = FindObjectOfType<EventSystem>();
	}

	/*
	IEnumerator Start(){

		mEventSystem = FindObjectOfType<EventSystem>();

		// 時間経過で自動的にタイトルに遷移する処理
		for (var t = 0f; t < 10f; t += Time.deltaTime)
		{
			yield return null;
		}

		FadeManager.Instance.fadeColor = Color.black;
		FadeManager.Instance.LoadLevel("Title", 1.0f);
	}
	*/
	#endregion

	#region public functions
	public void ToTitle()
	{
		mEventSystem.enabled = false;
		FadeManager.Instance.fadeColor = Color.black;
		FadeManager.Instance.LoadLevel("Title", 0.5f);
	}

	public void ToMain()
	{
		mEventSystem.enabled = false;
		SeManager.Instance.Play("Click");
		FadeManager.Instance.fadeColor = Color.white;
		FadeManager.Instance.LoadLevel("Main", 0.5f);
	}
	#endregion

}