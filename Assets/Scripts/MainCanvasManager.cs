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

        if(StaticManager.gameLength - time > 0f)
		{
			m_Time.text = "Time : " + (StaticManager.gameLength - time).ToString("00.0");
        }
        else {
			m_Time.text = "Time Over";
        }

		m_Eat.text = "EAT " + StaticManager.enemyCount;

        m_Evo.text = "EVO " + mPlayerParam.strRatioOfAmount();
    }
}
