using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour {

    Text counter;
    Text remainTime;

    float time = 0f;

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform) {
            if (child.name == "Counter") {
                counter = child.gameObject.GetComponent<Text>();
            }
            if (child.name == "Time") {
                remainTime = child.gameObject.GetComponent<Text>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        counter.text = "Num : " + StaticManager.enemyCount;

        if(StaticManager.gameLength - time > 0) {
            remainTime.text = "Time : " + (StaticManager.gameLength - time).ToString("000.00");
        }
        else {
            remainTime.text = "Time Over";
        }
        
    }
}
