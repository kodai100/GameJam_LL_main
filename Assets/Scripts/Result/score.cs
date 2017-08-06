/*
 リザルトのスコア表示
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class score : MonoBehaviour {
    

    float count = StaticManager.enemyCount;
   
    float i = 0;
    float ResultScore;

    void Awake()
    {
        ResultScore = count;
    }

	void Update () {

        if (i < ResultScore)
        {
            i++;
        }

        this.GetComponent<Text>().text = "Score : " + i;

	}
}
