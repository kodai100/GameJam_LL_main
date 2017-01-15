/*
 勝敗
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinOrLose : MonoBehaviour
{

    bool win;

    void Awake()
    {
        win = StaticManager.isWin; 
    }
    

    void Update()
    {

        if (win == true)
        {
            this.GetComponent<Text>().text = "YOU WIN";
        }

        if (win == false)
        {
            this.GetComponent<Text>().text = "YOU LOSE";
        }

    }
}
