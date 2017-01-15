/*
 リザルトのスコア表示
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class score : MonoBehaviour {
    //void showscore(float score)
    //{
        
    //}

    //float time = 0;

    //float ResultScore = MainGameController. getScore()

    float ResultScore = 200;
    float i = 0;

	void Update () {

        if (i < ResultScore)
        {
            i++;
        }

        this.GetComponent<Text>().text = "Score : " + i;

        //time += Time.deltaTime;
        //Debug.Log(time);
        //this.GetComponent<Text>().text = "score" + time;
	}
}
