using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour {

    Text counter;

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform) {
            if (child.name == "Counter") {
                counter = child.gameObject.GetComponent<Text>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        counter.text = "Num : " + StaticManager.enemyCount;
    }
}
