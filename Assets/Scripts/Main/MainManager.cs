using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

	private static MainManager instance;

	private float m_Stopwatch;

	public MainManager Instance {
		get {
			if (instance != this) {
				GameObject.DestroyImmediate (this);
				Debug.LogWarning ("There are two MainManager!!!");
			}
			else if (instance == null) {
				instance = this;
			}

			return instance;
		}
	}

	void Awake() {
		m_Stopwatch = 0f;
	}

	void Update() {
		m_Stopwatch += Time.deltaTime;

		if (m_Stopwatch > StaticManager.gameLength) {
			SceneManager.LoadScene ("Result");
		}
	}
}
