/*
  クリックによるタイトルからゲームシーンへの遷移
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleClick : MonoBehaviour
{
    void Update()
    {
        //左クリックで遷移
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Main");
        }
    }
}