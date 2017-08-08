using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour {

	[SerializeField]
	private Text m_Time;

	[SerializeField]
	private Text m_Eat;

	[SerializeField]
	private Text m_Evo;

    Text counter;
    Text remainTime;

    float time = 0f;

    PlayerParametter mPlayerParam;

    /*// Use this for initialization
	void Start () {
        foreach (Transform child in transform) {
            if (child.name == "Counter") {
                counter = child.gameObject.GetComponent<Text>();
            }
            if (child.name == "Time") {
                remainTime = child.gameObject.GetComponent<Text>();
            }
        }
    }*/

    void Start() {
        mPlayerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParametter>();
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;

        if(ConstantParam.GAME_TIME_SECONDS - time > 0f)
		{
			m_Time.text = "Time : " + (ConstantParam.GAME_TIME_SECONDS - time).ToString("00.0");
        }
        else {
			m_Time.text = "Time Over";
        }

		m_Eat.text = "EAT " + mPlayerParam.checkEnemyCount();

        m_Evo.text = "EVO " + mPlayerParam.strRatioOfAmount();
    }
}
