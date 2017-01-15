
/*
　リザルトからタイトルへの遷移処理
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    float time = 0;
    
    void Update()
    {
        //左クリックで遷移
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Title");
        }

        time += Time.deltaTime;

        if(time >= 10)
        {
            time = 0;

            SceneManager.LoadScene("Title");

        }
    }

}