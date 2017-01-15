using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

	private static MainManager instance;

	private float m_Stopwatch;

	public static MainManager Instance {
		get {
			if (instance == null)
			{
				instance = FindObjectOfType<MainManager>();
			}

			return instance;
		}
	}

	void Awake() {
		m_Stopwatch = 0f;
		BgmManager.Instance.TimeToFade = 2f;
		BgmManager.Instance.Play ("main");
	}

	void Update() {
		m_Stopwatch += Time.deltaTime;

		if (!StaticManager.isWin && m_Stopwatch > StaticManager.gameLength) {
			Win();
		}
	}

	public void Win()
	{
		BgmManager.Instance.Stop ();
		StaticManager.isWin = true;
		FadeManager.Instance.fadeColor = Color.white;
		FadeManager.Instance.LoadLevel("Result", 1.0f);
	}

	public void Lose()
	{
		BgmManager.Instance.Stop ();
		StaticManager.isWin = false;
		FadeManager.Instance.fadeColor = Color.black;
		FadeManager.Instance.LoadLevel("Result", 1.0f);
	}
}
