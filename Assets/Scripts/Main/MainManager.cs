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
		StaticManager.isWin = false;
	}

	void Start()
	{
		BgmManager.Instance.Stop();
		BgmManager.Instance.Play("main");
	}

	void Update() {
		m_Stopwatch += Time.deltaTime;
        
		if (!StaticManager.isWin && m_Stopwatch > ConstantParam.GAME_TIME_SECONDS) {
			Win();
		}
	}

    void EndProcess() {

        BgmManager.Instance.Stop();

        PlayerParametter param = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParametter>();
        StaticManager.resultEnemyCount = param.checkEnemyCount();
        StaticManager.resultAmount     = param.checkSumAmout();
    }

    public void Win(){
        EndProcess();
		StaticManager.isWin = true;
        FadeManager.Instance.fadeColor = Color.white;
		FadeManager.Instance.LoadLevel("Result", 1.0f);
	}

	public void Lose() {
        EndProcess();
        StaticManager.isWin = false;
		FadeManager.Instance.fadeColor = Color.black;
		FadeManager.Instance.LoadLevel("Result", 1.0f);
	}
}
