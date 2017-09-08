using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// タイトルシーンにおける遷移時の処理を管理
/// </summary>
public class TitleController : MonoBehaviour{

	#region private variables
	EventSystem mEventSystem;
	#endregion

	#region private functions
	void Start(){
		mEventSystem = FindObjectOfType<EventSystem>();
		BgmManager.Instance.Play ("title");
	}

	/// <summary>
	/// ゲーム終了コルーチン
	/// </summary>
	private IEnumerator EndCoroutine(){

		mEventSystem.enabled = false;
		BgmManager.Instance.TimeToFade = 0.5f;
		BgmManager.Instance.Stop();
		FadeManager.Instance.fadeColor = Color.black;
		FadeManager.Instance.LoadLevel(null, 0.5f);

		// 暗転が終わるまでのDelay
		for (var t = 0f; t < 0.5f; t += Time.deltaTime){
			yield return null;
		}
		Application.Quit ();

	}
	#endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			FadeManager.Instance.fadeColor = Color.white;
			FadeManager.Instance.LoadLevel("Main", 0.5f);
        }
    }

	#region public funtions
	/// <summary>
	/// メインシーンへの遷移処理
	/// </summary>
	public void ToMain(){

		mEventSystem.enabled = false;
		SeManager.Instance.Play("Click");
		BgmManager.Instance.TimeToFade = 0.5f;
		BgmManager.Instance.Stop();
		FadeManager.Instance.fadeColor = Color.white;
		FadeManager.Instance.LoadLevel("Main", 0.5f);
	}

	/// <summary>
	/// ゲーム終了時の処理
	/// </summary>
	public void End(){
		StartCoroutine ("EndCoroutine");
	}
	#endregion

}