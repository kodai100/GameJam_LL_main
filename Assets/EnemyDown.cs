/*
 リザルトのスコア表示
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyDown : MonoBehaviour
{

    //float Down = MainGameController.Down()

    float Down = 30;
    float i = 0;

    void Update()
    {

        if (i < Down)
        {
            i++;
        }

        this.GetComponent<Text>().text = "TOTAL" + i + "体吸収!!";

        //time += Time.deltaTime;
        //Debug.Log(time);
        //this.GetComponent<Text>().text = "score" + time;
    }
}
