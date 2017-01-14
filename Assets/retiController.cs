
/*
　リザルトからタイトルへの遷移処理
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class retiController : MonoBehaviour
{
    float time = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title");
        }

        time += 1;

        if(time >= 1000)
        {
            time = 0;

            SceneManager.LoadScene("Title");

        }
    }

}