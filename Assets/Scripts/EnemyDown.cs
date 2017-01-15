/*
 リザルトのスコア表示
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyDown : MonoBehaviour
{
    int Down = StaticManager.enemyCount;

    //float Down = MainGameController.Down()

    
    float i = 0;

    void Update()
    {

        if (i < Down)
        {
            i++;
        }

        this.GetComponent<Text>().text = "Eat : " + i + "!!";

        //time += Time.deltaTime;
        //Debug.Log(time);
        //this.GetComponent<Text>().text = "score" + time;
    }
}
