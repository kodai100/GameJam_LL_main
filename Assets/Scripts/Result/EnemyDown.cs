/*
 リザルトのスコア表示
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyDown : MonoBehaviour
{
    int Down = StaticManager.resultEnemyCount;    
    float i = 0f;

    void Update()
    {

        if (i < Down)
        {
            i++;
        }

        this.GetComponent<Text>().text = "Eat : " + i ;
    }
}
